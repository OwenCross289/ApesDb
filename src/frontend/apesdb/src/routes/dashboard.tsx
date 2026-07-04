import { Navigate, createFileRoute } from "@tanstack/react-router";
import { useAuth } from "../auth-context";

export const Route = createFileRoute("/dashboard")({
  component: DashboardComponent,
});

function DashboardComponent() {
  const { isAuthenticated, isLoading } = useAuth();

  if (isLoading) {
    return null;
  }

  if (!isAuthenticated) {
    return <Navigate to="/" />;
  }

  return (
    <main className="min-h-screen">
      <section className="mx-auto flex min-h-screen max-w-4xl flex-col justify-center gap-8 px-6">
        <h1 className="text-4xl font-semibold tracking-tight">Dashboard</h1>
        <p className="max-w-2xl text-lg text-muted-foreground">
          This page is only visible to signed-in users.
        </p>
      </section>
    </main>
  );
}
