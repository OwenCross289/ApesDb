import { useQuery } from "@tanstack/react-query";
import { notificationQueryKeys } from "./notification-query-keys";
import { fetchTeamInvite, TeamInviteNotFoundError } from "./notifications.api";

export function useTeamInvite(inviteId: string, enabled: boolean) {
  return useQuery({
    queryKey: notificationQueryKeys.invite(inviteId),
    queryFn: ({ signal }) => fetchTeamInvite(inviteId, signal),
    enabled,
    retry: (failureCount, requestError) => {
      if (requestError instanceof TeamInviteNotFoundError) {
        return false;
      }

      return failureCount < 2;
    },
  });
}
