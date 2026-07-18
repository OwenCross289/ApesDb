import { teamsResponseSchema, type Team } from "../teams.schemas";

export async function fetchTeams(signal: AbortSignal): Promise<Team[]> {
  const response = await fetch("/api/teams", {
    credentials: "include",
    signal,
  });

  if (!response.ok) {
    throw new Error(`Request failed with status ${response.status}.`);
  }

  return teamsResponseSchema.parse(await response.json());
}
