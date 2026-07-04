import { createFileRoute } from "@tanstack/react-router";
import { Button, ThemeModeSelector } from "@apesdb/ui";
import { appName } from "@apesdb/common";
import { ArrowRight, Database } from "lucide-react";
import { useAuth } from "../auth-context";

export const Route = createFileRoute("/")({
  component: IndexComponent,
});

function IndexComponent() {
  const { user, isAuthenticated, isLoading, login, logout } = useAuth();

  return (
    <main className="min-h-screen">
      <section className="mx-auto flex min-h-screen max-w-4xl flex-col justify-center gap-8 px-6">
        <header className="flex flex-wrap items-center justify-between gap-4">
          <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
            {appName}
          </p>
          <div className="flex items-center gap-4">
            <ThemeModeSelector />
            {isLoading ? null : isAuthenticated ? (
              <>
                <span className="text-sm text-muted-foreground">{user?.email}</span>
                <button
                  type="button"
                  onClick={() => logout()}
                  className="text-sm font-medium underline underline-offset-4"
                >
                  Log out
                </button>
              </>
            ) : (
              <button
                type="button"
                onClick={() => login()}
                className="text-sm font-medium underline underline-offset-4"
              >
                Log in
              </button>
            )}
          </div>
        </header>
        <div className="space-y-4">
          <h1 className="text-4xl font-semibold tracking-tight">ApesDb frontend scaffold</h1>
          <p className="max-w-2xl text-lg text-muted-foreground">
            The shadcn preset is wired through the shared UI package and rendered from the app.
          </p>
        </div>
        <div className="flex flex-wrap items-center gap-3">
          <Button type="button">
            <Database />
            Primary action
            <ArrowRight data-icon="inline-end" />
          </Button>
          <Button type="button" variant="secondary">
            Secondary
          </Button>
          <Button type="button" variant="outline">
            Outline
          </Button>
          <Button type="button" variant="ghost">
            Ghost
          </Button>
          <Button type="button" variant="destructive">
            Destructive
          </Button>
        </div>
      </section>
    </main>
  );
}
