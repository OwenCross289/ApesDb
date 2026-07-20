import {
  inferParserType,
  parseAsArrayOf,
  parseAsBoolean,
  parseAsInteger,
  parseAsString,
} from "nuqs";

const idArrayParser = parseAsArrayOf(parseAsInteger).withDefault([]);
const defaultGameTypeIdsParser = parseAsArrayOf(parseAsInteger).withDefault([0, 9, 10]);

export const gameFilterParsers = {
  search: parseAsString.withDefault(""),
  page: parseAsInteger.withDefault(1),
  gameTypeIds: defaultGameTypeIdsParser,
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
  const idFilterCount =
    filters.gameTypeIds.length +
    filters.gameStatusIds.length +
    filters.genreIds.length +
    filters.themeIds.length +
    filters.gameModeIds.length +
    filters.playerPerspectiveIds.length +
    filters.platformIds.length;
  const values = [
    filters.developer.trim().length > 0,
    filters.publisher.trim().length > 0,
    filters.collection.trim().length > 0,
    filters.franchise.trim().length > 0,
    filters.isSteam,
  ];

  return idFilterCount + values.filter(Boolean).length;
}

export function hasGameFilters(filters: GameFilters): boolean {
  return filters.search.trim().length > 0 || countAdvancedFilters(filters) > 0;
}

export const defaultAdvancedFilters: GameFilterPatch = {
  gameTypeIds: [0, 9, 10],
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
