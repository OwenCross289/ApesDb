using System.ComponentModel.DataAnnotations;

namespace ApesDb.Api.Options;

public sealed class CacheOptions
{
    public const string SectionName = "Cache";

    [Required]
    public required string ConnectionString { get; init; }

    [Required]
    public required string Password { get; init; }
}
