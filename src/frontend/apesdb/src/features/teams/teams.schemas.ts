import { z } from "zod";

export const teamKindSchema = z.enum(["solo", "group"]);

export const teamSchema = z.object({
  id: z.string(),
  name: z.string(),
  profilePictureUrl: z.string().nullable(),
  kind: teamKindSchema,
});

export const teamsResponseSchema = z.array(teamSchema);

export type Team = z.infer<typeof teamSchema>;
export type TeamKind = z.infer<typeof teamKindSchema>;
