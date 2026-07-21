import { z } from "zod";

export const gameReferenceSchema = z.object({
  id: z.number().int().nonnegative(),
  name: z.string(),
});

export const gameCoverSchema = z.object({
  imageId: z.string().nullable(),
  width: z.number().int().positive().nullable(),
  height: z.number().int().positive().nullable(),
  smallUrl: z.string().nullable(),
  largeUrl: z.string().nullable(),
});

export const gamePopularitySchema = z.object({
  rank: z.number().int().positive(),
  sourceRank: z.number().int().positive(),
  score: z.number().nonnegative(),
  type: gameReferenceSchema,
  calculatedAt: z.string().datetime({ offset: true }),
});

export const gameStorePageSchema = z.object({
  id: z.number().int().nonnegative(),
  source: gameReferenceSchema,
  platform: gameReferenceSchema.nullable(),
  externalId: z.string().nullable(),
  name: z.string().nullable(),
  url: z.string().nullable(),
  year: z.number().int().nonnegative().nullable(),
});

export const gameAddonSchema = z.object({
  type: z.string(),
  id: z.number().int().nonnegative(),
  name: z.string(),
  description: z.string().nullable(),
  releaseDate: z.string().datetime({ offset: true }).nullable(),
  coverSmallUrl: z.string().nullable(),
  coverLargeUrl: z.string().nullable(),
});

export const gameEditionSchema = z.object({
  id: z.number().int().nonnegative(),
  name: z.string(),
  description: z.string().nullable(),
  releaseDate: z.string().datetime({ offset: true }).nullable(),
  coverSmallUrl: z.string().nullable(),
  coverLargeUrl: z.string().nullable(),
});

export const gameDetailsSchema = z.object({
  id: z.number().int().nonnegative(),
  name: z.string(),
  slug: z.string().nullable(),
  description: z.string().nullable(),
  storyline: z.string().nullable(),
  releaseDate: z.string().datetime({ offset: true }).nullable(),
  totalRating: z.number().min(0).max(100).nullable(),
  totalRatingCount: z.number().int().nonnegative().nullable(),
  igdbUrl: z.string().nullable(),
  gameType: gameReferenceSchema.nullable(),
  gameStatus: gameReferenceSchema.nullable(),
  versionParentId: z.number().int().nonnegative().nullable(),
  cover: gameCoverSchema.nullable(),
  popularity: gamePopularitySchema.nullable(),
  developers: z.array(gameReferenceSchema),
  publishers: z.array(gameReferenceSchema),
  portingCompanies: z.array(gameReferenceSchema),
  supportingCompanies: z.array(gameReferenceSchema),
  storePages: z.array(gameStorePageSchema),
  editions: z.array(gameEditionSchema),
  addons: z.array(gameAddonSchema),
  genres: z.array(gameReferenceSchema),
  themes: z.array(gameReferenceSchema),
  gameModes: z.array(gameReferenceSchema),
  gameEngines: z.array(gameReferenceSchema),
  playerPerspectives: z.array(gameReferenceSchema),
  platforms: z.array(gameReferenceSchema),
  collections: z.array(gameReferenceSchema),
  franchises: z.array(gameReferenceSchema),
});

export type GameReference = z.infer<typeof gameReferenceSchema>;
export type GameStorePage = z.infer<typeof gameStorePageSchema>;
export type GameAddon = z.infer<typeof gameAddonSchema>;
export type GameEdition = z.infer<typeof gameEditionSchema>;
export type GameDetails = z.infer<typeof gameDetailsSchema>;
