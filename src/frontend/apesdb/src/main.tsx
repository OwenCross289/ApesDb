import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { RouterProvider, createRouter, defaultParseSearch } from "@tanstack/react-router";
import { ThemeProvider, Toaster } from "@apesdb/ui";
import { AuthGate, AuthProvider } from "./auth-context";
import { registerPwaUpdateListener } from "./register-pwa-update-listener";
import { routeTree } from "./routeTree.gen";
import "./styles.css";

registerPwaUpdateListener();

const queryClient = new QueryClient();

const router = createRouter({
  routeTree,
  parseSearch: (search) => defaultParseSearch(search.replaceAll("+", "%20")),
  context: {
    auth: undefined!,
  },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

const rootElement = document.getElementById("root");

if (!rootElement) {
  throw new Error("Root element #root was not found.");
}

createRoot(rootElement).render(
  <StrictMode>
    <ThemeProvider>
      <QueryClientProvider client={queryClient}>
        <AuthProvider>
          <AuthGate>{(auth) => <RouterProvider router={router} context={{ auth }} />}</AuthGate>
        </AuthProvider>
      </QueryClientProvider>
      <Toaster />
    </ThemeProvider>
  </StrictMode>,
);
