import { useState, type ReactNode } from "react";
import { Link } from "@tanstack/react-router";
import { Badge } from "@apesdb/ui";
import { Gamepad2 } from "lucide-react";
import { formatDate, formatDateTime } from "../../../lib/date";
import type { GameAddon, GameDetails, GameEdition, GameReference } from "./game-details.schemas";

type GameDetailsSectionsProps = {
  game: GameDetails;
};

function DetailSection({ title, children }: { title: string; children: ReactNode }) {
  return (
    <section className="border p-5 sm:p-6">
      <h2 className="mb-4 text-lg font-semibold tracking-tight">{title}</h2>
      {children}
    </section>
  );
}

function ReferenceGroup({
  label,
  values,
  emptyMessage,
}: {
  label: string;
  values: GameReference[];
  emptyMessage?: string;
}) {
  if (values.length === 0) {
    if (emptyMessage) {
      return (
        <div>
          <h3 className="mb-2 text-xs font-medium uppercase tracking-wide text-muted-foreground">
            {label}
          </h3>
          <p className="text-sm text-muted-foreground">{emptyMessage}</p>
        </div>
      );
    }

    return null;
  }

  return (
    <div>
      <h3 className="mb-2 text-xs font-medium uppercase tracking-wide text-muted-foreground">
        {label}
      </h3>
      <div className="flex flex-wrap gap-2">
        {values.map((value) => (
          <Badge key={value.id} variant="outline">
            {value.name}
          </Badge>
        ))}
      </div>
    </div>
  );
}

function OverviewSection({ game }: { game: GameDetails }) {
  if (!game.description && !game.storyline) {
    return null;
  }

  return (
    <DetailSection title="Overview">
      <div className="space-y-6">
        {game.description && (
          <p className="text-sm leading-7 whitespace-pre-line">{game.description}</p>
        )}
        {game.storyline && (
          <div>
            <h3 className="mb-2 text-sm font-semibold">Storyline</h3>
            <p className="text-sm leading-7 whitespace-pre-line text-muted-foreground">
              {game.storyline}
            </p>
          </div>
        )}
      </div>
    </DetailSection>
  );
}

function CategoriesSection({ game }: { game: GameDetails }) {
  const hasCategories =
    game.genres.length > 0 ||
    game.themes.length > 0 ||
    game.gameModes.length > 0 ||
    game.playerPerspectives.length > 0 ||
    game.platforms.length > 0 ||
    game.collections.length > 0 ||
    game.franchises.length > 0;

  if (!hasCategories) {
    return null;
  }

  return (
    <DetailSection title="Game details">
      <div className="grid gap-5 sm:grid-cols-2">
        <ReferenceGroup label="Platforms" values={game.platforms} />
        <ReferenceGroup label="Genres" values={game.genres} />
        <ReferenceGroup label="Themes" values={game.themes} />
        <ReferenceGroup label="Game modes" values={game.gameModes} />
        <ReferenceGroup label="Player perspectives" values={game.playerPerspectives} />
        <ReferenceGroup label="Collections" values={game.collections} />
        <ReferenceGroup label="Franchises" values={game.franchises} />
      </div>
    </DetailSection>
  );
}

function CompaniesSection({ game }: { game: GameDetails }) {
  return (
    <DetailSection title="Companies">
      <div className="grid gap-5 sm:grid-cols-2">
        <ReferenceGroup
          label="Developers"
          values={game.developers}
          emptyMessage="No developers found."
        />
        <ReferenceGroup
          label="Publishers"
          values={game.publishers}
          emptyMessage="No publishers found."
        />
        <ReferenceGroup
          label="Porting"
          values={game.portingCompanies}
          emptyMessage="No porting studios found."
        />
        <ReferenceGroup
          label="Supporting"
          values={game.supportingCompanies}
          emptyMessage="No supporting studios found."
        />
      </div>
    </DetailSection>
  );
}

function PopularitySection({ game }: { game: GameDetails }) {
  if (!game.popularity) {
    return null;
  }

  return (
    <DetailSection title="Popularity">
      <dl className="grid gap-4 text-sm sm:grid-cols-2 lg:grid-cols-4">
        <div>
          <dt className="text-xs uppercase tracking-wide text-muted-foreground">Rank</dt>
          <dd className="mt-1 font-medium">#{game.popularity.rank}</dd>
        </div>
        <div>
          <dt className="text-xs uppercase tracking-wide text-muted-foreground">Source rank</dt>
          <dd className="mt-1 font-medium">#{game.popularity.sourceRank}</dd>
        </div>
        <div>
          <dt className="text-xs uppercase tracking-wide text-muted-foreground">Score</dt>
          <dd className="mt-1 font-medium">
            {game.popularity.score.toLocaleString(undefined, { maximumFractionDigits: 4 })}
          </dd>
        </div>
        <div>
          <dt className="text-xs uppercase tracking-wide text-muted-foreground">
            {game.popularity.type.name}
          </dt>
          <dd className="mt-1 font-medium">{formatDateTime(game.popularity.calculatedAt)}</dd>
        </div>
      </dl>
    </DetailSection>
  );
}

type RelatedGame = GameAddon | GameEdition;

function RelatedGameCover({ game }: { game: RelatedGame }) {
  const coverUrl = game.coverSmallUrl ?? game.coverLargeUrl;
  const [imageAvailable, setImageAvailable] = useState(coverUrl !== null);

  if (coverUrl && imageAvailable) {
    return (
      <img
        alt=""
        className="h-28 w-21 shrink-0 bg-muted object-cover"
        loading="lazy"
        src={coverUrl}
        onError={() => setImageAvailable(false)}
      />
    );
  }

  return (
    <div className="flex h-28 w-21 shrink-0 items-center justify-center bg-muted text-muted-foreground">
      <Gamepad2 className="size-5" />
      <span className="sr-only">No cover available</span>
    </div>
  );
}

function RelatedGameCard({ game, label }: { game: RelatedGame; label: string }) {
  return (
    <Link
      className="group flex gap-4 border p-3 transition-colors hover:bg-muted/50 focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring/30"
      params={{ gameId: game.id.toString() }}
      to="/games/$gameId"
    >
      <RelatedGameCover key={game.coverSmallUrl ?? game.coverLargeUrl ?? "missing"} game={game} />
      <div className="min-w-0 py-1">
        <Badge variant="secondary">{label}</Badge>
        <h3 className="mt-2 font-medium group-hover:underline group-hover:underline-offset-4">
          {game.name}
        </h3>
        {game.releaseDate && (
          <p className="mt-1 text-xs text-muted-foreground">{formatDate(game.releaseDate)}</p>
        )}
        {game.description && (
          <p className="mt-2 line-clamp-3 text-sm text-muted-foreground">{game.description}</p>
        )}
      </div>
    </Link>
  );
}

function EditionsSection({ editions }: { editions: GameEdition[] }) {
  if (editions.length === 0) {
    return null;
  }

  return (
    <DetailSection title="Editions">
      <div className="grid gap-3 lg:grid-cols-2">
        {editions.map((edition) => (
          <RelatedGameCard game={edition} key={edition.id} label="Edition" />
        ))}
      </div>
    </DetailSection>
  );
}

function AddonsSection({ addons }: { addons: GameAddon[] }) {
  if (addons.length === 0) {
    return null;
  }

  return (
    <DetailSection title="Add-ons and expansions">
      <div className="grid gap-3 lg:grid-cols-2">
        {addons.map((addon) => (
          <RelatedGameCard game={addon} key={addon.id} label={addon.type} />
        ))}
      </div>
    </DetailSection>
  );
}

export function GameDetailsSections({ game }: GameDetailsSectionsProps) {
  return (
    <div className="grid gap-4">
      <OverviewSection game={game} />
      <CategoriesSection game={game} />
      <CompaniesSection game={game} />
      <PopularitySection game={game} />
      <EditionsSection editions={game.editions} />
      <AddonsSection addons={game.addons} />
    </div>
  );
}
