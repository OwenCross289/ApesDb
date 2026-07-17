import { z } from "zod";
import { teamDetailsSchema, type TeamDetails } from "./manage-team.schemas";

export class TeamNotFoundError extends Error {
  constructor() {
    super("The requested team was not found.");
    this.name = "TeamNotFoundError";
  }
}

export type InviteTeamMemberInput = {
  teamId: string;
  email: string;
};

const validationErrorSchema = z.object({
  message: z.string().optional(),
  errors: z.record(z.string(), z.array(z.string())).optional(),
});

async function inviteRequestError(response: Response): Promise<Error> {
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
    return new Error("Enter a valid email address and try again.");
  }

  return new Error(`Unable to send the invitation (status ${response.status}).`);
}

export async function fetchTeamDetails(teamId: string, signal: AbortSignal): Promise<TeamDetails> {
  const response = await fetch(`/api/teams/${encodeURIComponent(teamId)}`, {
    credentials: "include",
    signal,
  });

  if (response.status === 404) {
    throw new TeamNotFoundError();
  }

  if (!response.ok) {
    throw new Error(`Unable to load the team (status ${response.status}).`);
  }

  const result = teamDetailsSchema.safeParse(await response.json());

  if (!result.success) {
    throw new Error("The server returned an unexpected team response.");
  }

  return result.data;
}

export async function inviteTeamMember(input: InviteTeamMemberInput): Promise<void> {
  let response: Response;

  try {
    response = await fetch(`/api/teams/${encodeURIComponent(input.teamId)}/invites`, {
      method: "POST",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email: input.email }),
    });
  } catch {
    throw new Error("Unable to reach the server. Check your connection and try again.");
  }

  if (response.status === 404) {
    throw new TeamNotFoundError();
  }

  if (response.status !== 202) {
    throw await inviteRequestError(response);
  }
}
