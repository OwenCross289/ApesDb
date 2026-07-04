using System.Reflection;
using DbUp;

namespace ApesDb.Api.Data;

public static class MigrationRunner
{
    public static void Run(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Postgres connection string is not configured.");
        }

        if (string.Equals(connectionString, "InMemory", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var upgrader = DeployChanges
            .To.PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(
                typeof(MigrationRunner).Assembly,
                name =>
                    name.Contains("Data.Migrations", StringComparison.OrdinalIgnoreCase)
                    && name.EndsWith(".sql", StringComparison.OrdinalIgnoreCase)
            )
            .WithTransaction()
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw new InvalidOperationException("Database migration failed.", result.Error);
        }
    }
}
