import { z } from "zod";
import { teamProfilePictureSchema } from "../teams/teams.schemas";

export const notificationTypeSchema = z.enum(["TeamInvite"]);

export const notificationSchema = z.object({
  id: z.string(),
  type: notificationTypeSchema,
  resourceId: z.string(),
  createdAt: z.string(),
  readAt: z.string().nullable(),
  isUnread: z.boolean(),
  isActionable: z.boolean(),
});

export const notificationMetadataSchema = z.object({
  totalCount: z.number(),
  unreadCount: z.number(),
  actionableCount: z.number(),
  attentionCount: z.number(),
});

export const listNotificationsResponseSchema = z.object({
  items: z.array(notificationSchema),
  metadata: notificationMetadataSchema,
});

export const teamInviteSchema = z.object({
  id: z.string(),
  team: z
    .object({
      id: z.string(),
      name: z.string(),
      profilePicture: teamProfilePictureSchema.nullable(),
    })
    .transform(({ profilePicture, ...team }) => ({
      ...team,
      profilePictureUrl:
        profilePicture === null
          ? null
          : `data:${profilePicture.contentType};base64,${profilePicture.data}`,
    })),
  invitedBy: z.object({
    id: z.string(),
    name: z.string(),
    pictureUrl: z.string().nullable(),
  }),
  createdAt: z.string(),
});

export type Notification = z.infer<typeof notificationSchema>;
export type NotificationMetadata = z.infer<typeof notificationMetadataSchema>;
export type ListNotificationsResponse = z.infer<typeof listNotificationsResponseSchema>;
export type TeamInvite = z.infer<typeof teamInviteSchema>;
