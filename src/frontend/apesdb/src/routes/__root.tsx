import { Outlet, createRootRouteWithContext } from "@tanstack/react-router";
import { NuqsAdapter } from "nuqs/adapters/tanstack-router";
import type { AuthContextValue } from "../auth-context";

export const Route = createRootRouteWithContext<{
  auth: AuthContextValue;
}>()({
  component: RootComponent,
});

function RootComponent() {
  return (
    <div className="min-h-screen bg-background text-foreground">
      <NuqsAdapter>
        <Outlet />
      </NuqsAdapter>
    </div>
  );
}
