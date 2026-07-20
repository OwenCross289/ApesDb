import { Outlet, createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_app/games")({
  staticData: {
    breadcrumbs: [{ label: "Games", to: "/games" }],
  },
  component: GamesLayout,
});

function GamesLayout() {
  return <Outlet />;
}
