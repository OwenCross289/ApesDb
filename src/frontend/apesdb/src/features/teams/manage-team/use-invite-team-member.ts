import { useMutation } from "@tanstack/react-query";
import { inviteTeamMember } from "./manage-team.api";

export function useInviteTeamMember(teamId: string) {
  return useMutation({
    mutationFn: (email: string) => inviteTeamMember({ teamId, email }),
  });
}
