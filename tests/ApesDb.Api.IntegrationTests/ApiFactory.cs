using ApesDb.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ApesDb.Api.IntegrationTests;

public sealed class ApiFactory : WebApplicationFactory<IApiMarker> { }
