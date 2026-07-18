import { z } from "zod";
import { teamKindSchema, teamProfilePictureSchema } from "../teams.schemas";

export const teamMemberSchema = z.object({
  id: z.string(),
  name: z.string(),
  pictureUrl: z.string().nullable(),
});

export const teamDetailsSchema = z
  .object({
    id: z.string(),
    name: z.string(),
    profilePicture: teamProfilePictureSchema.nullable(),
    kind: teamKindSchema,
    members: z.array(teamMemberSchema),
  })
  .transform(({ profilePicture, ...team }) => ({
    ...team,
    profilePictureUrl:
      profilePicture === null
        ? null
        : `data:${profilePicture.contentType};base64,${profilePicture.data}`,
  }));

export type TeamDetails = z.infer<typeof teamDetailsSchema>;
export type TeamMember = z.infer<typeof teamMemberSchema>;
