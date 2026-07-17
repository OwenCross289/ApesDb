import { createContext, useContext, useMemo, useState, type ReactNode } from "react";
import type { Team } from "./teams.schemas";
import { useTeams } from "./select-team/use-teams";

export type TeamContextValue = {
  teams: Team[];
  activeTeam: Team | null;
  isLoading: boolean;
  error: string | null;
  setActiveTeamId: (teamId: string) => void;
};

const TeamContext = createContext<TeamContextValue | null>(null);

export function TeamProvider({ children }: { children: ReactNode }) {
  const { data, error, isLoading } = useTeams();
  const [activeTeamId, setActiveTeamId] = useState<string | null>(null);

  const teams = useMemo(() => data ?? [], [data]);

  const activeTeam = useMemo(() => {
    const selectedTeam = teams.find((team) => team.id === activeTeamId);

    if (selectedTeam) {
      return selectedTeam;
    }

    return teams.find((team) => team.kind === "solo") ?? teams[0] ?? null;
  }, [teams, activeTeamId]);

  const value: TeamContextValue = {
    teams,
    activeTeam,
    isLoading,
    error,
    setActiveTeamId,
  };

  return <TeamContext.Provider value={value}>{children}</TeamContext.Provider>;
}

export function useActiveTeam(): TeamContextValue {
  const context = useContext(TeamContext);

  if (!context) {
    throw new Error("useActiveTeam must be used within a TeamProvider.");
  }

  return context;
}
