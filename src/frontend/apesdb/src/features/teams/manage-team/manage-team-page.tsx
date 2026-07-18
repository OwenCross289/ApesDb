import { useEffect, useState } from "react";
import { Navigate } from "@tanstack/react-router";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  Badge,
  Button,
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
  Item,
  ItemContent,
  ItemDescription,
  ItemTitle,
  Skeleton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@apesdb/ui";
import { RefreshCw, User, UserPlus, Users } from "lucide-react";
import { useActiveTeam } from "../team-context";
import type { TeamKind } from "../teams.schemas";
import { InviteTeamMemberDialog } from "./invite-team-member-dialog";
import type { TeamDetails } from "./manage-team.schemas";
import { useTeamDetails } from "./use-team-details";

type ManageTeamPageProps = {
  teamId: string;
};

function teamKindLabel(kind: TeamKind): string {
  if (kind === "solo") {
    return "Solo team";
  }

  return "Group team";
}

function TeamKindIcon({ kind, className }: { kind: TeamKind; className?: string }) {
  if (kind === "solo") {
    return <User className={className} />;
  }

  return <Users className={className} />;
}

function ManageTeamSkeleton() {
  return (
    <div className="mx-auto grid w-full max-w-5xl gap-6">
      <div className="flex items-center gap-4">
        <Skeleton className="size-20 rounded-xl" />
        <div className="grid gap-2">
          <Skeleton className="h-3 w-24" />
          <Skeleton className="h-7 w-48" />
          <Skeleton className="h-5 w-20" />
        </div>
      </div>
      <Skeleton className="h-56 w-full rounded-lg" />
    </div>
  );
}

function TeamHeader({ team }: { team: TeamDetails }) {
  return (
    <header className="flex items-center gap-4">
      <Avatar className="size-20 rounded-xl">
        <AvatarImage alt={team.name} src={team.profilePictureUrl ?? undefined} />
        <AvatarFallback className="rounded-xl bg-muted text-muted-foreground">
          <TeamKindIcon className="size-7" kind={team.kind} />
        </AvatarFallback>
      </Avatar>
      <div className="grid min-w-0 gap-1.5">
        <p className="text-xs font-medium uppercase tracking-wider text-muted-foreground">
          Manage team
        </p>
        <h1 className="truncate text-2xl font-semibold tracking-tight">{team.name}</h1>
        <Badge className="w-fit" variant="secondary">
          {teamKindLabel(team.kind)}
        </Badge>
      </div>
    </header>
  );
}

function MembersCard({ team, onInvite }: { team: TeamDetails; onInvite: () => void }) {
  const memberCountLabel =
    team.members.length === 1 ? "1 accepted member" : `${team.members.length} accepted members`;

  return (
    <Card>
      <CardHeader>
        <CardTitle>Members</CardTitle>
        <CardDescription>{memberCountLabel}</CardDescription>
        {team.kind === "group" ? (
          <CardAction>
            <Button onClick={onInvite} type="button">
              <UserPlus data-icon="inline-start" />
              Invite member
            </Button>
          </CardAction>
        ) : null}
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Member</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {team.members.map((member) => (
              <TableRow key={member.id}>
                <TableCell>
                  <div className="flex items-center gap-2">
                    <Avatar className="size-7">
                      <AvatarImage alt={member.name} src={member.pictureUrl ?? undefined} />
                      <AvatarFallback className="bg-muted text-muted-foreground">
                        <User className="size-3.5" />
                      </AvatarFallback>
                    </Avatar>
                    <span className="font-medium">{member.name}</span>
                  </div>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </CardContent>
    </Card>
  );
}

export function ManageTeamPage({ teamId }: ManageTeamPageProps) {
  const teamDetails = useTeamDetails(teamId);
  const { setActiveTeamId } = useActiveTeam();
  const [isInviteDialogOpen, setIsInviteDialogOpen] = useState(false);
  const [accessLost, setAccessLost] = useState(false);

  useEffect(() => {
    if (teamDetails.data !== null) {
      setActiveTeamId(teamDetails.data.id);
    }
  }, [setActiveTeamId, teamDetails.data]);

  if (teamDetails.isInvalid || teamDetails.isNotFound || accessLost) {
    return <Navigate to="/" replace />;
  }

  if (teamDetails.isLoading) {
    return <ManageTeamSkeleton />;
  }

  if (teamDetails.error !== null) {
    return (
      <div className="mx-auto w-full max-w-5xl">
        <Item className="min-h-60 justify-center text-center" variant="outline">
          <ItemContent className="items-center">
            <ItemTitle>Team could not be loaded</ItemTitle>
            <ItemDescription>{teamDetails.error}</ItemDescription>
          </ItemContent>
          <Button onClick={teamDetails.retry} type="button" variant="outline">
            <RefreshCw data-icon="inline-start" />
            Retry
          </Button>
        </Item>
      </div>
    );
  }

  if (teamDetails.data === null) {
    return null;
  }

  const team = teamDetails.data;

  return (
    <main className="mx-auto grid w-full max-w-5xl gap-6">
      <TeamHeader team={team} />
      <MembersCard team={team} onInvite={() => setIsInviteDialogOpen(true)} />
      {team.kind === "group" ? (
        <InviteTeamMemberDialog
          open={isInviteDialogOpen}
          teamId={team.id}
          onAccessLost={() => setAccessLost(true)}
          onOpenChange={setIsInviteDialogOpen}
        />
      ) : null}
    </main>
  );
}
