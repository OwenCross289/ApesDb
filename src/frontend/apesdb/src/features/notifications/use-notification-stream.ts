import { useEffect } from "react";
import { useQueryClient } from "@tanstack/react-query";
import { z } from "zod";
import { notificationQueryKeys } from "./notification-query-keys";
import { notificationSchema, type ListNotificationsResponse } from "./notifications.schemas";
import {
  withAllNotificationsRead,
  withNotificationAdded,
  withNotificationResolved,
} from "./notifications.cache";

const readEventSchema = z.object({
  readAt: z.string(),
});

const resolvedEventSchema = z.object({
  resourceId: z.string(),
});

function parseEventData(event: Event): unknown {
  if (!(event instanceof MessageEvent) || typeof event.data !== "string") {
    return null;
  }

  try {
    return JSON.parse(event.data);
  } catch {
    return null;
  }
}

export function useNotificationStream() {
  const queryClient = useQueryClient();

  useEffect(() => {
    const source = new EventSource("/api/notifications/stream");

    const onCreated = (event: Event) => {
      const parsed = notificationSchema.safeParse(parseEventData(event));

      if (!parsed.success) {
        return;
      }

      queryClient.setQueryData<ListNotificationsResponse>(notificationQueryKeys.list, (current) =>
        withNotificationAdded(current, parsed.data),
      );
    };

    const onRead = (event: Event) => {
      const parsed = readEventSchema.safeParse(parseEventData(event));

      if (!parsed.success) {
        return;
      }

      queryClient.setQueryData<ListNotificationsResponse>(notificationQueryKeys.list, (current) =>
        withAllNotificationsRead(current, parsed.data.readAt),
      );
    };

    const onResolved = (event: Event) => {
      const parsed = resolvedEventSchema.safeParse(parseEventData(event));

      if (!parsed.success) {
        return;
      }

      queryClient.setQueryData<ListNotificationsResponse>(notificationQueryKeys.list, (current) =>
        withNotificationResolved(current, parsed.data.resourceId),
      );
    };

    source.addEventListener("notification.created", onCreated);
    source.addEventListener("notification.read", onRead);
    source.addEventListener("notification.resolved", onResolved);

    return () => {
      source.close();
    };
  }, [queryClient]);
}
