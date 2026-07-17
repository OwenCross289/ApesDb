import { useCallback } from "react";
import { useQuery } from "@tanstack/react-query";
import { fetchTeams } from "./teams.api";

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

export function useTeams() {
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: ["teams", "list"],
    queryFn: ({ signal }) => fetchTeams(signal),
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
