import { createRoute, lazyRouteComponent } from "@tanstack/react-router";
import { appRoute } from "../app-shell/app-shell-routes";

const homeRoute = createRoute({
  getParentRoute: () => appRoute,
  path: "/",
  component: lazyRouteComponent(() => import("./home-page"), "HomePage"),
});

export function addHomeRoutes() {
  return homeRoute;
}
