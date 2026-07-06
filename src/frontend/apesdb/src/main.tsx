import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { RouterProvider, createRouter } from "@tanstack/react-router";
import { ThemeProvider, Toaster } from "@apesdb/ui";
import { AuthProvider, useAuth } from "./auth-context";
import { registerPwaUpdateListener } from "./register-pwa-update-listener";
import { routeTree } from "./routeTree.gen";
import "./styles.css";

registerPwaUpdateListener();

const router = createRouter({
  routeTree,
  context: {
    auth: undefined!,
  },
});

declare module "@tanstack/react-router" {
  interface Register {
    router: typeof router;
  }
}

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <ThemeProvider>
      <AuthProvider>
        <AppRouter />
      </AuthProvider>
      <Toaster />
    </ThemeProvider>
  </StrictMode>,
);

function AppRouter() {
  const auth = useAuth();

  if (auth.isLoading) {
    return null;
  }

  return <RouterProvider router={router} context={{ auth }} />;
}
