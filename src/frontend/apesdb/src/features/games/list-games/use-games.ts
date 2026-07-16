import { useCallback, useMemo } from "react";
import { useQuery } from "@tanstack/react-query";
import { createGamesRequestUrl, fetchGameLookups, fetchGames } from "./games.api";
import type { GameFilters } from "./games-query-state";

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

export function useGames(filters: GameFilters) {
  const url = useMemo(() => createGamesRequestUrl(filters), [filters]);
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: ["games", "list", url],
    queryFn: ({ signal }) => fetchGames(url, signal),
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

export function useGameLookups() {
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: ["games", "lookups"],
    queryFn: ({ signal }) => fetchGameLookups(signal),
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
