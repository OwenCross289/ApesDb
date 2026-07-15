import { z } from "zod";

export const gameLookupValueSchema = z.object({
  id: z.number().int().nonnegative(),
  name: z.string(),
});

export const gameSchema = z.object({
  id: z.number().int().nonnegative(),
  coverSmallUrl: z.string().nullable(),
  name: z.string(),
  developers: z.array(z.string()),
  publishers: z.array(z.string()),
  isCoop: z.boolean(),
  isSteam: z.boolean(),
  gameType: gameLookupValueSchema.nullable(),
});

export const gamesResponseSchema = z.object({
  items: z.array(gameSchema),
  total: z.number().int().nonnegative(),
  filteredTotal: z.number().int().nonnegative(),
  page: z.number().int().positive(),
  pageSize: z.number().int().positive(),
});

export const gameLookupsSchema = z.object({
  gameTypes: z.array(gameLookupValueSchema),
  gameStatuses: z.array(gameLookupValueSchema),
  genres: z.array(gameLookupValueSchema),
  themes: z.array(gameLookupValueSchema),
  gameModes: z.array(gameLookupValueSchema),
  playerPerspectives: z.array(gameLookupValueSchema),
  platforms: z.array(gameLookupValueSchema),
});

export type Game = z.infer<typeof gameSchema>;
export type GamesResponse = z.infer<typeof gamesResponseSchema>;
export type GameLookupValue = z.infer<typeof gameLookupValueSchema>;
export type GameLookups = z.infer<typeof gameLookupsSchema>;
