import { useCallback, useEffect, useState } from "react";
import { useQueryStates } from "nuqs";
import { AdvancedFiltersSheet } from "./advanced-filters-sheet";
import { gamesPageSize } from "./games.api";
import {
  countAdvancedFilters,
  gameFilterParsers,
  hasGameFilters,
  type GameFilterPatch,
} from "./games-query-state";
import { GamesTable } from "./games-table";
import { GamesToolbar } from "./games-toolbar";
import { useGameLookups, useGames } from "./use-games";

export function GamesPage() {
  const [advancedFiltersOpen, setAdvancedFiltersOpen] = useState(false);
  const [filters, setFilters] = useQueryStates(gameFilterParsers, {
    clearOnDefault: true,
    history: "replace",
  });
  const games = useGames(filters);
  const lookups = useGameLookups();

  const updateFilters = useCallback(
    (patch: GameFilterPatch) => {
      void setFilters({ ...patch, page: 1 }, { history: "replace" });
    },
    [setFilters],
  );

  const updatePage = useCallback(
    (page: number) => {
      void setFilters({ page }, { history: "push" });
    },
    [setFilters],
  );

  useEffect(() => {
    if (!games.data) {
      return;
    }

    const pageCount = Math.max(1, Math.ceil(games.data.filteredTotal / gamesPageSize));
    const normalizedPage = Math.min(Math.max(1, filters.page), pageCount);
    if (normalizedPage !== filters.page) {
      void setFilters({ page: normalizedPage }, { history: "replace" });
    }
  }, [filters.page, games.data, setFilters]);

  return (
    <div className="mx-auto flex h-full min-h-0 w-full max-w-7xl flex-col gap-4 overflow-hidden">
      <div>
        <h1 className="text-2xl font-semibold tracking-tight">Games</h1>
        <p className="mt-1 text-sm text-muted-foreground">
          Browse and filter the synchronized game catalog.
        </p>
      </div>
      <GamesToolbar
        filters={filters}
        advancedFilterCount={countAdvancedFilters(filters)}
        onFiltersChange={updateFilters}
        onOpenAdvancedFilters={() => setAdvancedFiltersOpen(true)}
      />
      <GamesTable
        data={games.data}
        error={games.error}
        isLoading={games.isLoading}
        hasFilters={hasGameFilters(filters)}
        page={Math.max(1, filters.page)}
        onPageChange={updatePage}
        onRetry={games.retry}
      />
      <AdvancedFiltersSheet
        open={advancedFiltersOpen}
        filters={filters}
        lookups={lookups.data}
        lookupsError={lookups.error}
        lookupsLoading={lookups.isLoading}
        onOpenChange={setAdvancedFiltersOpen}
        onFiltersChange={updateFilters}
        onRetryLookups={lookups.retry}
      />
    </div>
  );
}
