import { createRoute, lazyRouteComponent } from "@tanstack/react-router";
import { rootRoute } from "../app-shell/app-shell-routes";

type LoginSearch = {
  redirect?: string;
  error?: "access-denied";
};

const loginRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: "login",
  component: lazyRouteComponent(() => import("./login-page"), "LoginPage"),
  validateSearch: (search: Record<string, unknown>): LoginSearch => ({
    redirect: isLocalReturnUrl(search.redirect) ? search.redirect : undefined,
    error: search.error === "access-denied" ? search.error : undefined,
  }),
});

export function addAuthRoutes() {
  return loginRoute;
}

function isLocalReturnUrl(value: unknown): value is string {
  return (
    typeof value === "string" &&
    value.startsWith("/") &&
    !value.startsWith("//") &&
    !value.startsWith("/\\")
  );
}
