import { createRouter, defaultParseSearch } from "@tanstack/react-router";
import { appRoute, rootRoute } from "./features/app-shell/app-shell-routes";
import { loginRoute } from "./features/auth/auth-routes";
import { gameDetailsRoute, gamesIndexRoute, gamesRoute } from "./features/games/games-routes";
import { homeRoute } from "./features/home/home-routes";
import { privacyRoute } from "./features/privacy/privacy-routes";
import { manageTeamRoute } from "./features/teams/teams-routes";

const routeTree = rootRoute.addChildren([
  appRoute.addChildren([
    homeRoute,
    gamesRoute.addChildren([gamesIndexRoute, gameDetailsRoute]),
    manageTeamRoute,
  ]),
  loginRoute,
  privacyRoute,
]);

export const router = createRouter({
  routeTree,
  parseSearch: (search) => defaultParseSearch(search.replaceAll("+", "%20")),
  context: {
    auth: undefined!,
  },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}
