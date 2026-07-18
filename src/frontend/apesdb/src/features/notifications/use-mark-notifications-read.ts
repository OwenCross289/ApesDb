import { useMutation, useQueryClient } from "@tanstack/react-query";
import { notificationQueryKeys } from "./notification-query-keys";
import { markAllNotificationsRead } from "./notifications.api";

export function useMarkNotificationsRead() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: () => markAllNotificationsRead(),
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: notificationQueryKeys.list });
    },
  });
}
