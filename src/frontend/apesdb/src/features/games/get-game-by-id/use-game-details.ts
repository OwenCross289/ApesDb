import { useCallback } from "react";
import { useQuery } from "@tanstack/react-query";
import { fetchGameDetails, GameDetailsNotFoundError } from "./game-details.api";

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

export function useGameDetails(gameId: number) {
  const isValid = Number.isInteger(gameId) && gameId > 0;
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: ["games", "details", gameId],
    queryFn: ({ signal }) => fetchGameDetails(gameId, signal),
    enabled: isValid,
    retry: (failureCount, requestError) => {
      if (requestError instanceof GameDetailsNotFoundError) {
        return false;
      }

      return failureCount < 2;
    },
  });
  const retry = useCallback(() => {
    void refetch();
  }, [refetch]);
  const isNotFound = error instanceof GameDetailsNotFoundError;

  return {
    data: data ?? null,
    error: error && !isNotFound ? errorMessage(error) : null,
    isInvalid: !isValid,
    isLoading,
    isNotFound,
    retry,
  };
}
