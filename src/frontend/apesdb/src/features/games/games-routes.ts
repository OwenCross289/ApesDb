import { createRoute, lazyRouteComponent } from "@tanstack/react-router";
import { appRoute } from "../app-shell/app-shell-routes";

export const gamesRoute = createRoute({
  getParentRoute: () => appRoute,
  path: "games",
  component: lazyRouteComponent(() => import("./games-layout"), "GamesLayout"),
  staticData: {
    breadcrumbs: [{ label: "Games", to: "/games" }],
  },
});

export const gamesIndexRoute = createRoute({
  getParentRoute: () => gamesRoute,
  path: "/",
  component: lazyRouteComponent(() => import("./list-games/games-page"), "GamesPage"),
});

export const gameDetailsRoute = createRoute({
  getParentRoute: () => gamesRoute,
  path: "$gameId",
  component: lazyRouteComponent(
    () => import("./get-game-by-id/game-details-page"),
    "GameDetailsPage",
  ),
  staticData: {
    breadcrumbs: [{ param: "gameId" }],
  },
});
