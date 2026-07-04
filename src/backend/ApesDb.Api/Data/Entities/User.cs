namespace ApesDb.Api.Data.Entities;

public sealed class User
{
    public Guid Id { get; init; }

    public required string Auth0Subject { get; init; }

    public required string Email { get; set; }

    public required string Name { get; set; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; set; }
}
