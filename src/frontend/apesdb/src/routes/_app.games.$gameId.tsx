import { createFileRoute } from "@tanstack/react-router";
import { GameDetailsPage } from "../features/games/get-game-by-id/game-details-page";

export const Route = createFileRoute("/_app/games/$gameId")({
  staticData: {
    breadcrumbs: [{ param: "gameId" }],
  },
  component: GameDetailsRoute,
});

function GameDetailsRoute() {
  const params = Route.useParams();
  return <GameDetailsPage gameId={Number(params.gameId)} />;
}
