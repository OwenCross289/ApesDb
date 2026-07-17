import { useMemo } from "react";
import { Link } from "@tanstack/react-router";
import {
  Badge,
  Button,
  Item,
  ItemContent,
  ItemDescription,
  ItemTitle,
  Pagination,
  PaginationContent,
  PaginationItem,
  Skeleton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@apesdb/ui";
import {
  createColumnHelper,
  flexRender,
  getCoreRowModel,
  useReactTable,
  type PaginationState,
} from "@tanstack/react-table";
import {
  ChevronsLeft,
  ChevronsRight,
  ChevronLeft,
  ChevronRight,
  Gamepad2,
  RefreshCw,
} from "lucide-react";
import { gamesPageSize } from "./games.api";
import type { Game, GamesResponse } from "./games.schemas";

type GamesTableProps = {
  data: GamesResponse | null;
  error: string | null;
  isLoading: boolean;
  hasFilters: boolean;
  page: number;
  onPageChange: (page: number) => void;
  onRetry: () => void;
};

const columnHelper = createColumnHelper<Game>();

function valueOrDash(value: string | undefined | null) {
  if (value && value.length > 0) {
    return value;
  }

  return <span className="text-muted-foreground">—</span>;
}

function GameCover({ game }: { game: Game }) {
  if (game.coverSmallUrl) {
    return (
      <img
        alt=""
        className="h-12 w-9 shrink-0 bg-muted object-cover"
        loading="lazy"
        src={game.coverSmallUrl}
      />
    );
  }

  return (
    <div className="flex h-12 w-9 shrink-0 items-center justify-center bg-muted text-muted-foreground">
      <Gamepad2 className="size-4" />
      <span className="sr-only">No cover available</span>
    </div>
  );
}

const columns = [
  columnHelper.display({
    id: "game",
    header: "Game",
    cell: ({ row }) => (
      <Link
        className="group flex min-w-56 items-center gap-3 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring/30"
        params={{ gameId: row.original.id.toString() }}
        to="/games/$gameId"
      >
        <GameCover game={row.original} />
        <span className="font-medium whitespace-normal group-hover:underline group-hover:underline-offset-4">
          {row.original.name}
        </span>
      </Link>
    ),
  }),
  columnHelper.accessor((game) => game.gameType?.name, {
    id: "gameType",
    header: "Type",
    cell: ({ getValue }) => valueOrDash(getValue()),
  }),
  columnHelper.accessor((game) => game.developers[0], {
    id: "developer",
    header: "Developer",
    cell: ({ getValue }) => valueOrDash(getValue()),
  }),
  columnHelper.accessor((game) => game.publishers[0], {
    id: "publisher",
    header: "Publisher",
    cell: ({ getValue }) => valueOrDash(getValue()),
  }),
  columnHelper.display({
    id: "availability",
    header: "Details",
    cell: ({ row }) => (
      <div className="flex gap-1">
        {row.original.isCoop && <Badge variant="secondary">Co-op</Badge>}
        {row.original.isSteam && <Badge variant="outline">Steam</Badge>}
        {!row.original.isCoop && !row.original.isSteam && (
          <span className="text-muted-foreground">—</span>
        )}
      </div>
    ),
  }),
];

function columnVisibilityClass(columnId: string) {
  if (columnId === "gameType") {
    return "hidden sm:table-cell";
  }

  if (columnId === "developer" || columnId === "publisher") {
    return "hidden lg:table-cell";
  }

  return "";
}

function GamesTableSkeleton() {
  return (
    <div className="min-h-0 flex-1 overflow-hidden border">
      <Table containerClassName="h-full overflow-auto">
        <TableHeader className="sticky top-0 z-10 bg-background">
          <TableRow>
            <TableHead>Game</TableHead>
            <TableHead className="hidden sm:table-cell">Type</TableHead>
            <TableHead className="hidden lg:table-cell">Developer</TableHead>
            <TableHead className="hidden lg:table-cell">Publisher</TableHead>
            <TableHead>Details</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {Array.from({ length: 8 }, (_, index) => (
            <TableRow key={index}>
              <TableCell>
                <div className="flex items-center gap-3">
                  <Skeleton className="h-12 w-9" />
                  <Skeleton className="h-4 w-44" />
                </div>
              </TableCell>
              <TableCell className="hidden sm:table-cell">
                <Skeleton className="h-4 w-24" />
              </TableCell>
              <TableCell className="hidden lg:table-cell">
                <Skeleton className="h-4 w-32" />
              </TableCell>
              <TableCell className="hidden lg:table-cell">
                <Skeleton className="h-4 w-32" />
              </TableCell>
              <TableCell>
                <Skeleton className="h-5 w-16" />
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}

function resultSummary(data: GamesResponse, hasFilters: boolean): string {
  if (data.filteredTotal === 0) {
    if (hasFilters) {
      return `Showing 0 matching games, from ${data.total.toLocaleString()} total.`;
    }

    return "Showing 0 games.";
  }

  const start = (data.page - 1) * data.pageSize + 1;
  const end = Math.min(data.page * data.pageSize, data.filteredTotal);
  if (hasFilters) {
    return `Showing ${start.toLocaleString()}–${end.toLocaleString()} of ${data.filteredTotal.toLocaleString()} matching games, from ${data.total.toLocaleString()} total.`;
  }

  return `Showing ${start.toLocaleString()}–${end.toLocaleString()} of ${data.total.toLocaleString()} games.`;
}

export function GamesTable({
  data,
  error,
  isLoading,
  hasFilters,
  page,
  onPageChange,
  onRetry,
}: GamesTableProps) {
  const pageCount = Math.max(1, Math.ceil((data?.filteredTotal ?? 0) / gamesPageSize));
  const pagination = useMemo<PaginationState>(
    () => ({ pageIndex: Math.max(0, page - 1), pageSize: gamesPageSize }),
    [page],
  );
  const table = useReactTable({
    data: data?.items ?? [],
    columns,
    getCoreRowModel: getCoreRowModel(),
    manualPagination: true,
    rowCount: data?.filteredTotal ?? 0,
    pageCount,
    state: { pagination },
    onPaginationChange: (updater) => {
      let next = updater;
      if (typeof updater === "function") {
        next = updater(pagination);
      }

      onPageChange(next.pageIndex + 1);
    },
  });

  let content;
  if (isLoading) {
    content = <GamesTableSkeleton />;
  } else if (error) {
    content = (
      <Item variant="outline" className="min-h-0 flex-1">
        <ItemContent>
          <ItemTitle>Games could not be loaded</ItemTitle>
          <ItemDescription>{error}</ItemDescription>
        </ItemContent>
        <Button type="button" variant="outline" onClick={onRetry}>
          <RefreshCw />
          Retry
        </Button>
      </Item>
    );
  } else if (!data) {
    content = <div className="min-h-0 flex-1" />;
  } else if (data.items.length === 0) {
    content = (
      <Item variant="outline" className="min-h-0 flex-1 justify-center text-center">
        <ItemContent className="items-center">
          <ItemTitle>No games found</ItemTitle>
          <ItemDescription>Try changing or clearing the active filters.</ItemDescription>
        </ItemContent>
      </Item>
    );
  } else {
    content = (
      <div className="min-h-0 flex-1 overflow-hidden border">
        <Table containerClassName="h-full overflow-auto">
          <TableHeader className="sticky top-0 z-10 bg-background">
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableHead className={columnVisibilityClass(header.column.id)} key={header.id}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(header.column.columnDef.header, header.getContext())}
                  </TableHead>
                ))}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows.map((row) => (
              <TableRow key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell className={columnVisibilityClass(cell.column.id)} key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    );
  }

  const paginationDisabled = isLoading || error !== null || data === null;

  return (
    <div className="flex min-h-0 flex-1 flex-col gap-3 overflow-hidden">
      {data && (
        <p className="shrink-0 text-xs text-muted-foreground" aria-live="polite">
          {resultSummary(data, hasFilters)}
        </p>
      )}
      {content}
      <div className="flex shrink-0 flex-col gap-2 sm:flex-row sm:items-center sm:justify-between">
        <span className="text-xs text-muted-foreground">
          Page {table.getState().pagination.pageIndex + 1} of {table.getPageCount()}
        </span>
        <Pagination className="mx-0 w-auto justify-start sm:justify-end">
          <PaginationContent>
            <PaginationItem>
              <Button
                aria-label="Go to first page"
                type="button"
                variant="outline"
                size="icon"
                disabled={paginationDisabled || !table.getCanPreviousPage()}
                onClick={() => table.firstPage()}
              >
                <ChevronsLeft />
              </Button>
            </PaginationItem>
            <PaginationItem>
              <Button
                aria-label="Go to previous page"
                type="button"
                variant="outline"
                size="icon"
                disabled={paginationDisabled || !table.getCanPreviousPage()}
                onClick={() => table.previousPage()}
              >
                <ChevronLeft />
              </Button>
            </PaginationItem>
            <PaginationItem>
              <Button
                aria-label="Go to next page"
                type="button"
                variant="outline"
                size="icon"
                disabled={paginationDisabled || !table.getCanNextPage()}
                onClick={() => table.nextPage()}
              >
                <ChevronRight />
              </Button>
            </PaginationItem>
            <PaginationItem>
              <Button
                aria-label="Go to last page"
                type="button"
                variant="outline"
                size="icon"
                disabled={paginationDisabled || !table.getCanNextPage()}
                onClick={() => table.lastPage()}
              >
                <ChevronsRight />
              </Button>
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      </div>
    </div>
  );
}
