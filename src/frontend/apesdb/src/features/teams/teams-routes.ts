import { createRoute, lazyRouteComponent } from "@tanstack/react-router";
import { appRoute } from "../app-shell/app-shell-routes";

const manageTeamRoute = createRoute({
  getParentRoute: () => appRoute,
  path: "teams/$teamId/manage",
  component: lazyRouteComponent(() => import("./manage-team/manage-team-page"), "ManageTeamPage"),
  staticData: {
    breadcrumbs: [{ label: "Teams" }, { param: "teamId" }, { label: "Manage" }],
  },
});

export function addTeamRoutes() {
  return manageTeamRoute;
}
