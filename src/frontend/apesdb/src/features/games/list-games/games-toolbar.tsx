import { Badge, Button, Field, FieldLabel, Switch } from "@apesdb/ui";
import { ListFilter, Search } from "lucide-react";
import type { GameFilterPatch, GameFilters } from "./games-query-state";
import { DebouncedFilterInput } from "./debounced-filter-input";

type GamesToolbarProps = {
  filters: GameFilters;
  advancedFilterCount: number;
  onFiltersChange: (patch: GameFilterPatch) => void;
  onOpenAdvancedFilters: () => void;
};

export function GamesToolbar({
  filters,
  advancedFilterCount,
  onFiltersChange,
  onOpenAdvancedFilters,
}: GamesToolbarProps) {
  return (
    <div className="flex flex-col gap-3 sm:flex-row sm:items-center">
      <div className="relative min-w-0 flex-1 sm:max-w-md">
        <Search className="pointer-events-none absolute top-1/2 left-2 size-4 -translate-y-1/2 text-muted-foreground" />
        <DebouncedFilterInput
          aria-label="Search games"
          className="h-8 pl-8"
          placeholder="Search games…"
          value={filters.search}
          onValueChange={(search) => onFiltersChange({ search })}
        />
      </div>
      <div className="flex items-center gap-3">
        <Field orientation="horizontal" className="w-auto">
          <Switch
            id="games-coop-filter"
            checked={filters.isCoop}
            onCheckedChange={(isCoop) => onFiltersChange({ isCoop })}
          />
          <FieldLabel htmlFor="games-coop-filter">Co-op only</FieldLabel>
        </Field>
        <Button
          type="button"
          variant={advancedFilterCount > 0 ? "secondary" : "outline"}
          size="lg"
          onClick={onOpenAdvancedFilters}
        >
          <ListFilter />
          Advanced
          {advancedFilterCount > 0 && <Badge>{advancedFilterCount}</Badge>}
        </Button>
      </div>
    </div>
  );
}
