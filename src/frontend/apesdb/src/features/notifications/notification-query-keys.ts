export const notificationQueryKeys = {
  list: ["notifications", "list"] as const,
  invite: (inviteId: string) => ["notifications", "invite", inviteId] as const,
};
