import { useCallback, useEffect, useMemo, useState } from "react";
import { createGamesRequestUrl, fetchGameLookups, fetchGames } from "./games.api";
import type { GameFilters } from "./games-query-state";
import type { GameLookups, GamesResponse } from "./games.schemas";

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

export function useGames(filters: GameFilters) {
  const url = useMemo(() => createGamesRequestUrl(filters), [filters]);
  const [retryToken, setRetryToken] = useState(0);
  const [data, setData] = useState<GamesResponse | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const controller = new AbortController();
    setIsLoading(true);
    setError(null);

    void fetchGames(url, controller.signal)
      .then((response) => {
        setData(response);
      })
      .catch((requestError: unknown) => {
        if (!controller.signal.aborted) {
          setError(errorMessage(requestError));
        }
      })
      .finally(() => {
        if (!controller.signal.aborted) {
          setIsLoading(false);
        }
      });

    return () => controller.abort();
  }, [retryToken, url]);

  const retry = useCallback(() => setRetryToken((value) => value + 1), []);
  return { data, error, isLoading, retry };
}

export function useGameLookups() {
  const [retryToken, setRetryToken] = useState(0);
  const [data, setData] = useState<GameLookups | null>(null);
  const [error, setError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const controller = new AbortController();
    setIsLoading(true);
    setError(null);

    void fetchGameLookups(controller.signal)
      .then((response) => {
        setData(response);
      })
      .catch((requestError: unknown) => {
        if (!controller.signal.aborted) {
          setError(errorMessage(requestError));
        }
      })
      .finally(() => {
        if (!controller.signal.aborted) {
          setIsLoading(false);
        }
      });

    return () => controller.abort();
  }, [retryToken]);

  const retry = useCallback(() => setRetryToken((value) => value + 1), []);
  return { data, error, isLoading, retry };
}
