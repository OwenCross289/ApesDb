import { z } from "zod";

export const teamKindSchema = z.enum(["solo", "group"]);

export const teamProfilePictureSchema = z.object({
  contentType: z.string(),
  data: z.string(),
});

export const teamSchema = z.object({
  id: z.string(),
  name: z.string(),
  profilePicture: teamProfilePictureSchema.nullable(),
  kind: teamKindSchema,
});

export const teamsResponseSchema = z.array(teamSchema);

export type Team = z.infer<typeof teamSchema>;
export type TeamKind = z.infer<typeof teamKindSchema>;
