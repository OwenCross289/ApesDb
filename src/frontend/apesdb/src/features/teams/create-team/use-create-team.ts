import { useMutation, useQueryClient } from "@tanstack/react-query";
import { teamQueryKeys } from "../team-query-keys";
import type { Team } from "../teams.schemas";
import { createTeam } from "./create-team.api";

export function useCreateTeam() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createTeam,
    onSuccess: (createdTeam) => {
      queryClient.setQueryData<Team[]>(teamQueryKeys.list, (teams) => {
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

      void queryClient.invalidateQueries({ queryKey: teamQueryKeys.list });
    },
  });
}
