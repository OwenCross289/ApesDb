import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { Button } from "@apesdb/ui";
import { appName } from "@apesdb/common";
import { ArrowRight, Database } from "lucide-react";
import "./styles.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <main className="min-h-screen bg-background text-foreground">
      <section className="mx-auto flex min-h-screen max-w-4xl flex-col justify-center gap-8 px-6">
        <p className="text-sm font-medium uppercase tracking-wider text-muted-foreground">
          {appName}
        </p>
        <div className="space-y-4">
          <h1 className="text-4xl font-semibold tracking-tight">ApesDb frontend scaffold</h1>
          <p className="max-w-2xl text-lg text-muted-foreground">
            The shadcn preset is wired through the shared UI package and rendered from the app.
          </p>
        </div>
        <div className="flex flex-wrap items-center gap-3">
          <Button>
            <Database />
            Primary action
            <ArrowRight data-icon="inline-end" />
          </Button>
          <Button variant="secondary">Secondary</Button>
          <Button variant="outline">Outline</Button>
          <Button variant="ghost">Ghost</Button>
          <Button variant="destructive">Destructive</Button>
        </div>
      </section>
    </main>
  </StrictMode>,
);
