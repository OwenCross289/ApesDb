import {
  listNotificationsResponseSchema,
  teamInviteSchema,
  type ListNotificationsResponse,
  type TeamInvite,
} from "./notifications.schemas";

export class TeamInviteNotFoundError extends Error {
  constructor() {
    super("This invitation is no longer available.");
    this.name = "TeamInviteNotFoundError";
  }
}

export class TeamInviteConflictError extends Error {
  constructor() {
    super("This invitation has already been accepted.");
    this.name = "TeamInviteConflictError";
  }
}

export async function fetchNotifications(signal: AbortSignal): Promise<ListNotificationsResponse> {
  const response = await fetch("/api/notifications", {
    credentials: "include",
    signal,
  });

  if (!response.ok) {
    throw new Error(`Unable to load notifications (status ${response.status}).`);
  }

  const result = listNotificationsResponseSchema.safeParse(await response.json());

  if (!result.success) {
    throw new Error("The server returned an unexpected notifications response.");
  }

  return result.data;
}

export async function markAllNotificationsRead(): Promise<void> {
  const response = await fetch("/api/notifications/read", {
    method: "POST",
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error(`Unable to mark notifications as read (status ${response.status}).`);
  }
}

export async function fetchTeamInvite(inviteId: string, signal: AbortSignal): Promise<TeamInvite> {
  const response = await fetch(`/api/teams/invites/${encodeURIComponent(inviteId)}`, {
    credentials: "include",
    signal,
  });

  if (response.status === 404) {
    throw new TeamInviteNotFoundError();
  }

  if (!response.ok) {
    throw new Error(`Unable to load the invitation (status ${response.status}).`);
  }

  const result = teamInviteSchema.safeParse(await response.json());

  if (!result.success) {
    throw new Error("The server returned an unexpected invitation response.");
  }

  return result.data;
}

export async function respondToTeamInvite(inviteId: string, accept: boolean): Promise<void> {
  const response = await fetch(`/api/teams/invites/${encodeURIComponent(inviteId)}/respond`, {
    method: "POST",
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ accept }),
  });

  if (response.status === 404) {
    throw new TeamInviteNotFoundError();
  }

  if (response.status === 409) {
    throw new TeamInviteConflictError();
  }

  if (response.status !== 204) {
    throw new Error(`Unable to respond to the invitation (status ${response.status}).`);
  }
}
