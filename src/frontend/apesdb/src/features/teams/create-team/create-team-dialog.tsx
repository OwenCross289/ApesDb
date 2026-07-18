import { useEffect, useRef, useState, type ChangeEvent, type FormEvent } from "react";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  Button,
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  Field,
  FieldDescription,
  FieldError,
  FieldLabel,
  Input,
} from "@apesdb/ui";
import { ImageIcon, Loader2, Trash2, Users } from "lucide-react";
import { toast } from "sonner";
import { useCreateTeam } from "./use-create-team";

const maximumNameLength = 128;
const maximumProfilePictureLength = 5 * 1024 * 1024;
const supportedProfilePictureTypes = new Set(["image/jpeg", "image/png", "image/webp"]);

type CreateTeamDialogProps = {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCreated: (teamId: string) => void;
};

export function CreateTeamDialog({ open, onOpenChange, onCreated }: CreateTeamDialogProps) {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [name, setName] = useState("");
  const [profilePicture, setProfilePicture] = useState<File | null>(null);
  const [profilePicturePreviewUrl, setProfilePicturePreviewUrl] = useState<string | null>(null);
  const [nameError, setNameError] = useState<string | null>(null);
  const [profilePictureError, setProfilePictureError] = useState<string | null>(null);
  const createTeam = useCreateTeam();

  useEffect(() => {
    return () => {
      if (profilePicturePreviewUrl !== null) {
        URL.revokeObjectURL(profilePicturePreviewUrl);
      }
    };
  }, [profilePicturePreviewUrl]);

  function resetForm() {
    setName("");
    setProfilePicture(null);
    setProfilePicturePreviewUrl(null);
    setNameError(null);
    setProfilePictureError(null);
    createTeam.reset();

    if (fileInputRef.current !== null) {
      fileInputRef.current.value = "";
    }
  }

  function handleOpenChange(nextOpen: boolean) {
    if (createTeam.isPending && !nextOpen) {
      return;
    }

    if (!nextOpen) {
      resetForm();
    }

    onOpenChange(nextOpen);
  }

  function handleNameChange(event: ChangeEvent<HTMLInputElement>) {
    setName(event.target.value);
    setNameError(null);

    if (createTeam.isError) {
      createTeam.reset();
    }
  }

  function handleProfilePictureChange(event: ChangeEvent<HTMLInputElement>) {
    const file = event.target.files?.[0];

    if (!file) {
      return;
    }

    if (!supportedProfilePictureTypes.has(file.type)) {
      setProfilePictureError("Choose a JPEG, PNG, or WebP image.");
      event.target.value = "";
      return;
    }

    if (file.size > maximumProfilePictureLength) {
      setProfilePictureError("The profile picture must be 5 MB or smaller.");
      event.target.value = "";
      return;
    }

    setProfilePicture(file);
    setProfilePicturePreviewUrl(URL.createObjectURL(file));
    setProfilePictureError(null);

    if (createTeam.isError) {
      createTeam.reset();
    }
  }

  function removeProfilePicture() {
    setProfilePicture(null);
    setProfilePicturePreviewUrl(null);
    setProfilePictureError(null);

    if (fileInputRef.current !== null) {
      fileInputRef.current.value = "";
    }

    if (createTeam.isError) {
      createTeam.reset();
    }
  }

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const trimmedName = name.trim();

    if (trimmedName.length === 0) {
      setNameError("Enter a team name.");
      return;
    }

    if (trimmedName.length > maximumNameLength) {
      setNameError(`Team names must be ${maximumNameLength} characters or fewer.`);
      return;
    }

    if (profilePictureError !== null) {
      return;
    }

    setNameError(null);

    try {
      const createdTeam = await createTeam.mutateAsync({
        name: trimmedName,
        profilePicture,
      });

      toast.success(`${createdTeam.name} created`);
      onCreated(createdTeam.id);
      resetForm();
      onOpenChange(false);
    } catch {
      // The mutation error is rendered below the fields.
    }
  }

  const requestError = createTeam.error instanceof Error ? createTeam.error.message : null;

  return (
    <Dialog open={open} onOpenChange={handleOpenChange}>
      <DialogContent showCloseButton={!createTeam.isPending}>
        <form className="grid gap-4" onSubmit={handleSubmit}>
          <DialogHeader>
            <DialogTitle>Create team</DialogTitle>
            <DialogDescription>
              Create a shared team for organizing games with other members.
            </DialogDescription>
          </DialogHeader>

          <Field data-invalid={nameError !== null}>
            <FieldLabel htmlFor="create-team-name">Team name</FieldLabel>
            <Input
              id="create-team-name"
              autoComplete="off"
              autoFocus
              disabled={createTeam.isPending}
              maxLength={maximumNameLength}
              onChange={handleNameChange}
              placeholder="Team name"
              value={name}
              aria-invalid={nameError !== null}
              aria-describedby={nameError !== null ? "create-team-name-error" : undefined}
            />
            {nameError !== null ? (
              <FieldError id="create-team-name-error">{nameError}</FieldError>
            ) : null}
          </Field>

          <Field data-invalid={profilePictureError !== null}>
            <FieldLabel htmlFor="create-team-picture">Profile picture</FieldLabel>
            <div className="flex items-center gap-3">
              <Avatar className="size-14 rounded-lg">
                <AvatarImage
                  alt="Team profile picture preview"
                  src={profilePicturePreviewUrl ?? undefined}
                />
                <AvatarFallback className="rounded-lg bg-muted text-muted-foreground">
                  {profilePicture === null ? (
                    <Users className="size-5" />
                  ) : (
                    <ImageIcon className="size-5" />
                  )}
                </AvatarFallback>
              </Avatar>
              <div className="grid min-w-0 flex-1 gap-2">
                <Input
                  ref={fileInputRef}
                  id="create-team-picture"
                  accept="image/jpeg,image/png,image/webp"
                  disabled={createTeam.isPending}
                  onChange={handleProfilePictureChange}
                  type="file"
                  aria-invalid={profilePictureError !== null}
                  aria-describedby="create-team-picture-description"
                />
                {profilePicture !== null || profilePictureError !== null ? (
                  <Button
                    className="w-fit"
                    disabled={createTeam.isPending}
                    onClick={removeProfilePicture}
                    size="sm"
                    type="button"
                    variant="ghost"
                  >
                    <Trash2 data-icon="inline-start" />
                    {profilePicture !== null ? "Remove picture" : "Clear selection"}
                  </Button>
                ) : null}
              </div>
            </div>
            <FieldDescription id="create-team-picture-description">
              JPEG, PNG, or WebP. Maximum size 5 MB.
            </FieldDescription>
            {profilePictureError !== null ? <FieldError>{profilePictureError}</FieldError> : null}
          </Field>

          {requestError !== null ? <FieldError>{requestError}</FieldError> : null}

          <DialogFooter>
            <Button
              disabled={createTeam.isPending}
              onClick={() => handleOpenChange(false)}
              type="button"
              variant="outline"
            >
              Cancel
            </Button>
            <Button disabled={createTeam.isPending} type="submit">
              {createTeam.isPending ? <Loader2 className="animate-spin" /> : null}
              {createTeam.isPending ? "Creating…" : "Create team"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
