import { createFileRoute } from "@tanstack/react-router";
import { ListDetailsPage } from "../features/lists/get-list-by-id/list-details-page";

export const Route = createFileRoute("/_app/teams/$teamId/lists/$listId")({
  component: ListDetailsRoute,
});

function ListDetailsRoute() {
  const params = Route.useParams();
  return <ListDetailsPage teamId={params.teamId} listId={params.listId} />;
}
