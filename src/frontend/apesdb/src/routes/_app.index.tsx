import { createFileRoute } from "@tanstack/react-router";
import { appName } from "@apesdb/common";
import { useAuth } from "../auth-context";

export const Route = createFileRoute("/_app/")({
  component: WelcomeComponent,
});

function WelcomeComponent() {
  const { user } = useAuth();

  return (
    <main className="flex min-h-[calc(100svh-6rem)] items-center justify-center">
      <div className="w-full max-w-lg space-y-3 text-center">
        <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
          {appName}
        </p>
        <h1 className="text-3xl font-semibold tracking-tight">Welcome back</h1>
        <p className="text-muted-foreground">
          Logged in as <span className="font-medium text-foreground">{user?.email}</span>.
        </p>
      </div>
    </main>
  );
}
