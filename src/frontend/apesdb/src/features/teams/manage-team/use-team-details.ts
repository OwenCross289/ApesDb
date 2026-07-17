import { useCallback } from "react";
import { useQuery } from "@tanstack/react-query";
import { z } from "zod";
import { teamQueryKeys } from "../team-query-keys";
import { fetchTeamDetails, TeamNotFoundError } from "./manage-team.api";

const teamIdSchema = z.uuid();

function errorMessage(error: unknown): string {
  if (error instanceof Error) {
    return error.message;
  }

  return "An unexpected error occurred.";
}

export function useTeamDetails(teamId: string) {
  const isValid = teamIdSchema.safeParse(teamId).success;
  const { data, error, isLoading, refetch } = useQuery({
    queryKey: teamQueryKeys.details(teamId),
    queryFn: ({ signal }) => fetchTeamDetails(teamId, signal),
    enabled: isValid,
    retry: (failureCount, requestError) => {
      if (requestError instanceof TeamNotFoundError) {
        return false;
      }

      return failureCount < 2;
    },
  });
  const retry = useCallback(() => {
    void refetch();
  }, [refetch]);
  const isNotFound = error instanceof TeamNotFoundError;

  return {
    data: data ?? null,
    error: error && !isNotFound ? errorMessage(error) : null,
    isInvalid: !isValid,
    isLoading,
    isNotFound,
    retry,
  };
}
