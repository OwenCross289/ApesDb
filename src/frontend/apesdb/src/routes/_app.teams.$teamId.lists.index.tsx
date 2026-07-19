import { createFileRoute } from "@tanstack/react-router";
import { ListsPage } from "../features/lists/list-lists/lists-page";

export const Route = createFileRoute("/_app/teams/$teamId/lists/")({
  component: ListsRoute,
});

function ListsRoute() {
  const params = Route.useParams();
  return <ListsPage teamId={params.teamId} />;
}
