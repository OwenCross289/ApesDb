import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { Button } from "@apesdb/ui";
import { appName } from "@apesdb/common";
import "./styles.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <main className="min-h-screen bg-zinc-950 text-zinc-50">
      <section className="mx-auto flex min-h-screen max-w-4xl flex-col justify-center gap-6 px-6">
        <p className="text-sm font-medium uppercase tracking-wider text-emerald-300">{appName}</p>
        <div className="space-y-4">
          <h1 className="text-4xl font-semibold">ApesDb frontend scaffold</h1>
          <p className="max-w-2xl text-lg text-zinc-300">
            React, Vite, Nx, Oxc, and shared workspace packages are wired together.
          </p>
        </div>
        <div>
          <Button>Ready</Button>
        </div>
      </section>
    </main>
  </StrictMode>,
);
