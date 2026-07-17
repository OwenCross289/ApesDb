namespace ApesDb.Api.Features.Teams;

public sealed record TeamProfilePictureResponse(string ContentType, byte[] Data);

public sealed record TeamMemberResponse(Guid Id, string Name, string? PictureUrl);

public sealed record TeamResponse(
    Guid Id,
    string Name,
    string Kind,
    DateTime CreatedAt,
    TeamProfilePictureResponse? ProfilePicture,
    TeamMemberResponse[] Members
);

public static class TeamResponseFactory
{
    public static TeamProfilePictureResponse? CreateProfilePicture(byte[]? data)
    {
        if (data is null)
        {
            return null;
        }

        return new TeamProfilePictureResponse("image/webp", data);
    }

    public static string CreateKind(ApesDb.Domain.Entities.TeamKind kind)
    {
        if (kind == ApesDb.Domain.Entities.TeamKind.Solo)
        {
            return "solo";
        }

        return "group";
    }
}
