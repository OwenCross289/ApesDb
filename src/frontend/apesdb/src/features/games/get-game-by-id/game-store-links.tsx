import { Tooltip, TooltipContent, TooltipTrigger, buttonVariants } from "@apesdb/ui";
import { BsNintendoSwitch } from "react-icons/bs";
import { FaPlaystation, FaSteam, FaTwitch, FaXbox } from "react-icons/fa";
import { SiEpicgames } from "react-icons/si";
import type { GameStorePage } from "./game-details.schemas";

type GameStoreLinksProps = {
  storePages: GameStorePage[];
};

type StorePlatform = "xbox" | "playstation" | "steam" | "epic" | "twitch" | "nintendo";

function safeHttpUrl(value: string | null): string | null {
  if (!value) {
    return null;
  }

  try {
    const url = new URL(value);
    if (url.protocol === "http:" || url.protocol === "https:") {
      return url.toString();
    }
  } catch {
    return null;
  }

  return null;
}

function storePlatform(sourceName: string): StorePlatform | null {
  const normalizedName = sourceName.toLowerCase();
  if (normalizedName.includes("amazon")) {
    return null;
  }

  if (normalizedName.includes("microsoft") || normalizedName.includes("xbox")) {
    return "xbox";
  }

  if (normalizedName.includes("playstation")) {
    return "playstation";
  }

  if (normalizedName.includes("steam")) {
    return "steam";
  }

  if (normalizedName.includes("epic")) {
    return "epic";
  }

  if (normalizedName.includes("twitch")) {
    return "twitch";
  }

  if (normalizedName.includes("nintendo")) {
    return "nintendo";
  }

  return null;
}

function StoreIcon({ platform }: { platform: StorePlatform }) {
  if (platform === "xbox") {
    return <FaXbox aria-hidden="true" />;
  }

  if (platform === "playstation") {
    return <FaPlaystation aria-hidden="true" />;
  }

  if (platform === "steam") {
    return <FaSteam aria-hidden="true" />;
  }

  if (platform === "epic") {
    return <SiEpicgames aria-hidden="true" />;
  }

  if (platform === "twitch") {
    return <FaTwitch aria-hidden="true" />;
  }

  return <BsNintendoSwitch aria-hidden="true" />;
}

function storeDescription(storePage: GameStorePage): string {
  const parts = [storePage.source.name];
  if (storePage.platform) {
    parts.push(storePage.platform.name);
  }

  if (storePage.name && storePage.name !== storePage.source.name) {
    parts.push(storePage.name);
  }

  if (storePage.year !== null) {
    parts.push(storePage.year.toString());
  }

  if (storePage.externalId) {
    parts.push(`ID ${storePage.externalId}`);
  }

  return parts.join(" / ");
}

export function GameStoreLinks({ storePages }: GameStoreLinksProps) {
  const visibleStorePages: Array<{
    storePage: GameStorePage;
    platform: StorePlatform;
    href: string;
  }> = [];
  const visiblePlatforms = new Set<StorePlatform>();

  for (const storePage of storePages) {
    const platform = storePlatform(storePage.source.name);
    const href = safeHttpUrl(storePage.url);
    if (!platform || !href || visiblePlatforms.has(platform)) {
      continue;
    }

    visiblePlatforms.add(platform);
    visibleStorePages.push({ storePage, platform, href });
  }

  if (visibleStorePages.length === 0) {
    return null;
  }

  return (
    <div className="flex flex-wrap items-center gap-2" aria-label="Store links" role="group">
      {visibleStorePages.map(({ storePage, platform, href }) => {
        const description = storeDescription(storePage);
        const label = `Open ${storePage.source.name} in a new tab`;

        return (
          <Tooltip key={platform}>
            <TooltipTrigger
              render={
                <a
                  aria-label={label}
                  className={buttonVariants({ variant: "outline", size: "icon-lg" })}
                  href={href}
                  rel="noopener noreferrer"
                  target="_blank"
                />
              }
            >
              <StoreIcon platform={platform} />
            </TooltipTrigger>
            <TooltipContent>{description}</TooltipContent>
          </Tooltip>
        );
      })}
    </div>
  );
}
