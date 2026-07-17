import { useState, type ChangeEvent, type FormEvent } from "react";
import {
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
import { Loader2 } from "lucide-react";
import { toast } from "sonner";
import { TeamNotFoundError } from "./manage-team.api";
import { useInviteTeamMember } from "./use-invite-team-member";

const maximumEmailLength = 256;

type InviteTeamMemberDialogProps = {
  teamId: string;
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onAccessLost: () => void;
};

export function InviteTeamMemberDialog({
  teamId,
  open,
  onOpenChange,
  onAccessLost,
}: InviteTeamMemberDialogProps) {
  const [email, setEmail] = useState("");
  const [emailError, setEmailError] = useState<string | null>(null);
  const inviteTeamMember = useInviteTeamMember(teamId);

  function resetForm() {
    setEmail("");
    setEmailError(null);
    inviteTeamMember.reset();
  }

  function handleOpenChange(nextOpen: boolean) {
    if (inviteTeamMember.isPending && !nextOpen) {
      return;
    }

    if (!nextOpen) {
      resetForm();
    }

    onOpenChange(nextOpen);
  }

  function handleEmailChange(event: ChangeEvent<HTMLInputElement>) {
    setEmail(event.target.value);
    setEmailError(null);

    if (inviteTeamMember.isError) {
      inviteTeamMember.reset();
    }
  }

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const normalizedEmail = email.trim();

    if (normalizedEmail.length === 0) {
      setEmailError("Enter an email address.");
      return;
    }

    if (normalizedEmail.length > maximumEmailLength) {
      setEmailError(`Email addresses must be ${maximumEmailLength} characters or fewer.`);
      return;
    }

    setEmailError(null);

    try {
      await inviteTeamMember.mutateAsync(normalizedEmail);
      toast.success("Invitation sent", {
        description: "If an account exists for that address, they will receive an invitation.",
      });
      resetForm();
      onOpenChange(false);
    } catch (error) {
      if (error instanceof TeamNotFoundError) {
        onAccessLost();
      }
    }
  }

  let requestError: string | null = null;
  if (
    inviteTeamMember.error instanceof Error &&
    !(inviteTeamMember.error instanceof TeamNotFoundError)
  ) {
    requestError = inviteTeamMember.error.message;
  }

  return (
    <Dialog open={open} onOpenChange={handleOpenChange}>
      <DialogContent showCloseButton={!inviteTeamMember.isPending}>
        <form className="grid gap-4" onSubmit={handleSubmit}>
          <DialogHeader>
            <DialogTitle>Invite member</DialogTitle>
            <DialogDescription>Invite an existing ApesDb user to join this team.</DialogDescription>
          </DialogHeader>

          <Field data-invalid={emailError !== null}>
            <FieldLabel htmlFor="invite-team-member-email">Email address</FieldLabel>
            <Input
              id="invite-team-member-email"
              autoComplete="email"
              autoFocus
              disabled={inviteTeamMember.isPending}
              maxLength={maximumEmailLength}
              onChange={handleEmailChange}
              placeholder="member@example.com"
              required
              type="email"
              value={email}
              aria-invalid={emailError !== null}
              aria-describedby="invite-team-member-description"
            />
            <FieldDescription id="invite-team-member-description">
              Invitations can only be delivered to existing accounts.
            </FieldDescription>
            {emailError !== null ? <FieldError>{emailError}</FieldError> : null}
          </Field>

          {requestError !== null ? <FieldError>{requestError}</FieldError> : null}

          <DialogFooter>
            <Button
              disabled={inviteTeamMember.isPending}
              onClick={() => handleOpenChange(false)}
              type="button"
              variant="outline"
            >
              Cancel
            </Button>
            <Button disabled={inviteTeamMember.isPending} type="submit">
              {inviteTeamMember.isPending ? <Loader2 className="animate-spin" /> : null}
              {inviteTeamMember.isPending ? "Sending…" : "Send invitation"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}
