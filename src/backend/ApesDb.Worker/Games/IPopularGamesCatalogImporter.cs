using ApesDb.Igdb.Sdk.Models;

namespace ApesDb.Worker.Games;

public interface IPopularGamesCatalogImporter
{
    Task ImportAsync(IgdbPopularCatalog catalog, CancellationToken cancellationToken = default);
}
