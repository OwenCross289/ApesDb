import { createFileRoute } from "@tanstack/react-router";
import { ManageTeamPage } from "../features/teams/manage-team/manage-team-page";

export const Route = createFileRoute("/_app/teams/$teamId/manage")({
  component: ManageTeamRoute,
});

function ManageTeamRoute() {
  const params = Route.useParams();
  return <ManageTeamPage teamId={params.teamId} />;
}
