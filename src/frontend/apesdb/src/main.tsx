import { StrictMode } from "react";
import { createRoot, type Root } from "react-dom/client";
import { RouterProvider, createRouter } from "@tanstack/react-router";
import { ThemeProvider, Toaster } from "@apesdb/ui";
import { AuthProvider, useAuth } from "./auth-context";
import { registerPwaUpdateListener } from "./register-pwa-update-listener";
import { routeTree } from "./routeTree.gen";
import "./styles.css";

type HotData = {
  root?: Root;
};

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

const rootElement = document.getElementById("root");

if (!rootElement) {
  throw new Error("Root element #root was not found.");
}

const hotData = import.meta.hot?.data as HotData | undefined;
const root = hotData?.root ?? createRoot(rootElement);

if (hotData) {
  hotData.root = root;
}

root.render(
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
