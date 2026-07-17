import { gameDetailsSchema, type GameDetails } from "./game-details.schemas";

export class GameDetailsNotFoundError extends Error {
  constructor() {
    super("The requested game was not found.");
    this.name = "GameDetailsNotFoundError";
  }
}

export async function fetchGameDetails(gameId: number, signal: AbortSignal): Promise<GameDetails> {
  const response = await fetch(`/api/games/${gameId}`, {
    credentials: "include",
    signal,
  });

  if (response.status === 404) {
    throw new GameDetailsNotFoundError();
  }

  if (!response.ok) {
    throw new Error(`Request failed with status ${response.status}.`);
  }

  return gameDetailsSchema.parse(await response.json());
}
