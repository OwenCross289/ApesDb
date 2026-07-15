import {
  inferParserType,
  parseAsArrayOf,
  parseAsBoolean,
  parseAsInteger,
  parseAsString,
} from "nuqs";

const idArrayParser = parseAsArrayOf(parseAsInteger).withDefault([]);
const mainGameTypeIdsParser = parseAsArrayOf(parseAsInteger).withDefault([1]);

export const gameFilterParsers = {
  search: parseAsString.withDefault(""),
  isCoop: parseAsBoolean.withDefault(false),
  page: parseAsInteger.withDefault(1),
  gameTypeIds: mainGameTypeIdsParser,
  gameStatusIds: idArrayParser,
  genreIds: idArrayParser,
  themeIds: idArrayParser,
  gameModeIds: idArrayParser,
  playerPerspectiveIds: idArrayParser,
  platformIds: idArrayParser,
  developer: parseAsString.withDefault(""),
  publisher: parseAsString.withDefault(""),
  collection: parseAsString.withDefault(""),
  franchise: parseAsString.withDefault(""),
  isSteam: parseAsBoolean.withDefault(false),
};

export type GameFilters = inferParserType<typeof gameFilterParsers>;
export type GameFilterPatch = Partial<GameFilters>;

export function countAdvancedFilters(filters: GameFilters): number {
  const values = [
    filters.gameTypeIds.length > 0,
    filters.gameStatusIds.length > 0,
    filters.genreIds.length > 0,
    filters.themeIds.length > 0,
    filters.gameModeIds.length > 0,
    filters.playerPerspectiveIds.length > 0,
    filters.platformIds.length > 0,
    filters.developer.trim().length > 0,
    filters.publisher.trim().length > 0,
    filters.collection.trim().length > 0,
    filters.franchise.trim().length > 0,
    filters.isSteam,
  ];

  return values.filter(Boolean).length;
}

export function hasGameFilters(filters: GameFilters): boolean {
  return filters.search.trim().length > 0 || filters.isCoop || countAdvancedFilters(filters) > 0;
}

export const defaultAdvancedFilters: GameFilterPatch = {
  gameTypeIds: [1],
  gameStatusIds: [],
  genreIds: [],
  themeIds: [],
  gameModeIds: [],
  playerPerspectiveIds: [],
  platformIds: [],
  developer: "",
  publisher: "",
  collection: "",
  franchise: "",
  isSteam: false,
};
