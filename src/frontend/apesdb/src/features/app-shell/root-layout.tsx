import { Outlet } from "@tanstack/react-router";
import { NuqsAdapter } from "nuqs/adapters/tanstack-router";

export function RootLayout() {
  return (
    <div className="min-h-screen bg-background text-foreground">
      <NuqsAdapter>
        <Outlet />
      </NuqsAdapter>
    </div>
  );
}
