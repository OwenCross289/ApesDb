import tailwindcss from "@tailwindcss/vite";
import { TanStackRouterVite } from "@tanstack/router-plugin/vite";
import react from "@vitejs/plugin-react";
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "vite";

export default defineConfig({
  plugins: [
    TanStackRouterVite({ target: "react", autoCodeSplitting: true }),
    react(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      "@apesdb/common": fileURLToPath(new URL("../common/src/index.ts", import.meta.url)),
      "@apesdb/ui/lib": fileURLToPath(new URL("../ui/src/lib", import.meta.url)),
      "@apesdb/ui/hooks": fileURLToPath(new URL("../ui/src/hooks", import.meta.url)),
      "@apesdb/ui": fileURLToPath(new URL("../ui/src/index.ts", import.meta.url)),
    },
  },
  root: __dirname,
  build: {
    outDir: "dist",
    emptyOutDir: true,
  },
});
