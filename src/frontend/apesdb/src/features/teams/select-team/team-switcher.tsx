import { useState } from "react";
import { Link } from "@tanstack/react-router";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuSub,
  DropdownMenuSubContent,
  DropdownMenuSubTrigger,
  DropdownMenuTrigger,
  SidebarMenuButton,
  Skeleton,
  useSidebar,
} from "@apesdb/ui";
import { Check, ChevronsUpDown, Plus, Settings, User, Users } from "lucide-react";
import { CreateTeamDialog } from "../create-team/create-team-dialog";
import { useActiveTeam } from "../team-context";
import type { Team, TeamKind } from "../teams.schemas";

function teamKindLabel(kind: TeamKind): string {
  if (kind === "solo") {
    return "Solo team";
  }

  return "Group team";
}

function TeamKindIcon({ kind }: { kind: TeamKind }) {
  if (kind === "solo") {
    return <User className="size-4" />;
  }

  return <Users className="size-4" />;
}

function TeamAvatar({ team, className }: { team: Team; className?: string }) {
  return (
    <Avatar className={className}>
      <AvatarImage alt={team.name} src={team.profilePictureUrl ?? undefined} />
      <AvatarFallback className="rounded-md bg-sidebar-accent text-sidebar-foreground">
        <TeamKindIcon kind={team.kind} />
      </AvatarFallback>
    </Avatar>
  );
}

export function TeamSwitcher() {
  const { isMobile } = useSidebar();
  const { teams, activeTeam, isLoading, error, setActiveTeamId } = useActiveTeam();
  const [isCreateTeamDialogOpen, setIsCreateTeamDialogOpen] = useState(false);

  if (isLoading) {
    return (
      <SidebarMenuButton size="lg" disabled>
        <Skeleton className="size-8 rounded-md" />
        <div className="grid flex-1 gap-1 text-left">
          <Skeleton className="h-4 w-28" />
          <Skeleton className="h-3 w-20" />
        </div>
      </SidebarMenuButton>
    );
  }

  if (error !== null || activeTeam === null) {
    return (
      <SidebarMenuButton size="lg" disabled tooltip="Teams unavailable">
        <div className="flex aspect-square size-8 items-center justify-center rounded-md bg-sidebar-accent text-sidebar-foreground">
          <Users className="size-4" />
        </div>
        <div className="grid flex-1 text-left leading-tight">
          <span className="truncate font-medium">Teams unavailable</span>
        </div>
      </SidebarMenuButton>
    );
  }

  return (
    <>
      <DropdownMenu>
        <SidebarMenuButton
          render={<DropdownMenuTrigger />}
          size="lg"
          tooltip={activeTeam.name}
          className="data-popup-open:bg-sidebar-accent data-popup-open:text-sidebar-accent-foreground"
        >
          <TeamAvatar className="size-8 rounded-md" team={activeTeam} />
          <div className="grid flex-1 text-left leading-tight">
            <span className="truncate font-medium">{activeTeam.name}</span>
            <span className="truncate text-xs text-muted-foreground">
              {teamKindLabel(activeTeam.kind)}
            </span>
          </div>
          <ChevronsUpDown className="ml-auto size-4" />
        </SidebarMenuButton>
        <DropdownMenuContent
          align="start"
          side={isMobile ? "bottom" : "right"}
          sideOffset={8}
          className="min-w-56"
        >
          <DropdownMenuGroup>
            <DropdownMenuLabel>Teams</DropdownMenuLabel>
            {teams.map((team) => {
              const isActive = team.id === activeTeam.id;

              return (
                <DropdownMenuItem key={team.id} onClick={() => setActiveTeamId(team.id)}>
                  <TeamAvatar className="size-6 rounded-md" team={team} />
                  <div className="grid min-w-0 flex-1 text-left leading-tight">
                    <span className="truncate">{team.name}</span>
                    <span className="truncate text-xs text-muted-foreground">
                      {teamKindLabel(team.kind)}
                    </span>
                  </div>
                  {isActive ? <Check className="ml-auto size-4" /> : null}
                </DropdownMenuItem>
              );
            })}
          </DropdownMenuGroup>
          <DropdownMenuSeparator />
          <DropdownMenuSub>
            <DropdownMenuSubTrigger>
              <Settings />
              Manage team
            </DropdownMenuSubTrigger>
            <DropdownMenuSubContent className="min-w-56">
              {teams.map((team) => (
                <DropdownMenuItem
                  key={team.id}
                  render={<Link params={{ teamId: team.id }} to="/teams/$teamId/manage" />}
                >
                  <TeamAvatar className="size-6 rounded-md" team={team} />
                  <div className="grid min-w-0 flex-1 text-left leading-tight">
                    <span className="truncate">{team.name}</span>
                    <span className="truncate text-xs text-muted-foreground">
                      {teamKindLabel(team.kind)}
                    </span>
                  </div>
                </DropdownMenuItem>
              ))}
            </DropdownMenuSubContent>
          </DropdownMenuSub>
          <DropdownMenuItem onClick={() => setIsCreateTeamDialogOpen(true)}>
            <Plus />
            Create team
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
      <CreateTeamDialog
        open={isCreateTeamDialogOpen}
        onOpenChange={setIsCreateTeamDialogOpen}
        onCreated={setActiveTeamId}
      />
    </>
  );
}
