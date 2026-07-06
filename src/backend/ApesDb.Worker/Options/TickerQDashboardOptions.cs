using System.ComponentModel.DataAnnotations;

namespace ApesDb.Worker.Options;

public sealed class TickerQDashboardOptions
{
    public const string SectionName = "TickerQ:Dashboard";

    [Required]
    public string BasePath { get; init; } = "/tickerq/dashboard";

    [Required]
    public string Username { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}
