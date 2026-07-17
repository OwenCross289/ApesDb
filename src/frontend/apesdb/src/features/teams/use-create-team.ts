import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createTeam } from "./teams.api";
import type { Team } from "./teams.schemas";
import { teamsQueryKey } from "./use-teams";

export function useCreateTeam() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createTeam,
    onSuccess: (createdTeam) => {
      queryClient.setQueryData<Team[]>(teamsQueryKey, (teams) => {
        if (!teams) {
          return [createdTeam];
        }

        const existingTeamIndex = teams.findIndex((team) => team.id === createdTeam.id);

        if (existingTeamIndex === -1) {
          return [...teams, createdTeam];
        }

        return teams.map((team) => {
          if (team.id === createdTeam.id) {
            return createdTeam;
          }

          return team;
        });
      });

      void queryClient.invalidateQueries({ queryKey: teamsQueryKey });
    },
  });
}
