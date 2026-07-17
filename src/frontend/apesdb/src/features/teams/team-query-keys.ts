export const teamQueryKeys = {
  list: ["teams", "list"] as const,
  details: (teamId: string) => ["teams", "details", teamId] as const,
};
