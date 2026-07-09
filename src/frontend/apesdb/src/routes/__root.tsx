import { Link, Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import { appName } from "@apesdb/common";
import type { AuthContextValue } from "../auth-context";

export const Route = createRootRouteWithContext<{
  auth: AuthContextValue;
}>()({
  component: RootComponent,
});

function RootComponent() {
  return (
    <div className="flex min-h-screen flex-col bg-background text-foreground">
      <div className="flex-1">
        <Outlet />
      </div>
      <footer className="flex h-14 items-center border-t bg-background px-6">
        <div className="mx-auto flex w-full max-w-7xl items-center justify-between gap-4 text-sm text-muted-foreground">
          <p>
            &copy; {new Date().getFullYear()} {appName}
          </p>
          <Link
            to="/privacy"
            className="font-medium text-foreground underline-offset-4 hover:underline"
          >
            Privacy Policy
          </Link>
        </div>
      </footer>
    </div>
  );
}
