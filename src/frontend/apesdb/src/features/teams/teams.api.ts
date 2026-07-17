import { z } from "zod";
import { teamSchema, teamsResponseSchema, type Team } from "./teams.schemas";

export type CreateTeamInput = {
  name: string;
  profilePicture: File | null;
};

const validationErrorSchema = z.object({
  message: z.string().optional(),
  errors: z.record(z.string(), z.array(z.string())).optional(),
});

async function createRequestError(response: Response): Promise<Error> {
  if (response.status === 413) {
    return new Error("The profile picture must be 5 MB or smaller.");
  }

  try {
    const result = validationErrorSchema.safeParse(await response.json());

    if (result.success && result.data.errors) {
      const message = Object.values(result.data.errors).flat()[0];

      if (message) {
        return new Error(message);
      }
    }

    if (result.success && result.data.message) {
      return new Error(result.data.message);
    }
  } catch {
    // Fall through to the status-based message when the response is not JSON.
  }

  if (response.status === 400) {
    return new Error("Check the team details and try again.");
  }

  return new Error(`Unable to create the team (status ${response.status}).`);
}

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

export async function createTeam(input: CreateTeamInput): Promise<Team> {
  const formData = new FormData();
  formData.set("Name", input.name);

  if (input.profilePicture !== null) {
    formData.set("ProfilePicture", input.profilePicture);
  }

  let response: Response;

  try {
    response = await fetch("/api/teams", {
      method: "POST",
      credentials: "include",
      body: formData,
    });
  } catch {
    throw new Error("Unable to reach the server. Check your connection and try again.");
  }

  if (!response.ok) {
    throw await createRequestError(response);
  }

  const result = teamSchema.safeParse(await response.json());

  if (!result.success) {
    throw new Error("The server returned an unexpected team response.");
  }

  return result.data;
}
