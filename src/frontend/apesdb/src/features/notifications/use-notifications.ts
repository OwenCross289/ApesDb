import { useCallback } from "react";
import { useQuery } from "@tanstack/react-query";
import { notificationQueryKeys } from "./notification-query-keys";
import { fetchNotifications } from "./notifications.api";

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

export function useNotifications() {
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: notificationQueryKeys.list,
    queryFn: ({ signal }) => fetchNotifications(signal),
  });
  const retry = useCallback(() => {
    void refetch();
  }, [refetch]);

  return {
    data: data ?? null,
    error: error ? errorMessage(error) : null,
    isLoading,
    retry,
  };
}
