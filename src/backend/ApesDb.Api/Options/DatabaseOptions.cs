using System.ComponentModel.DataAnnotations;

namespace ApesDb.Api.Options;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    [Required]
    public required string ConnectionString { get; init; }
}
