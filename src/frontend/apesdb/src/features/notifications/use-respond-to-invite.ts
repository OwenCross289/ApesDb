import { useMutation, useQueryClient } from "@tanstack/react-query";
import { teamQueryKeys } from "../teams/team-query-keys";
import { notificationQueryKeys } from "./notification-query-keys";
import { respondToTeamInvite } from "./notifications.api";

type RespondToInviteInput = {
  inviteId: string;
  accept: boolean;
};

export function useRespondToInvite() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (input: RespondToInviteInput) => respondToTeamInvite(input.inviteId, input.accept),
    onSuccess: (_data, input) => {
      void queryClient.invalidateQueries({ queryKey: notificationQueryKeys.list });

      if (input.accept) {
        void queryClient.invalidateQueries({ queryKey: teamQueryKeys.list });
      }
    },
    onError: () => {
      void queryClient.invalidateQueries({ queryKey: notificationQueryKeys.list });
    },
  });
}
