import { createRoute, lazyRouteComponent } from "@tanstack/react-router";
import { rootRoute } from "../app-shell/app-shell-routes";

const privacyRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "privacy",
  component: lazyRouteComponent(() => import("./privacy-policy-page"), "PrivacyPolicyPage"),
});

export function addPrivacyRoutes() {
  return privacyRoute;
}
