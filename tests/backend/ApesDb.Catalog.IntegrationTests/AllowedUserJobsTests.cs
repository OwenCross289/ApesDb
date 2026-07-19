using ApesDb.Worker.Users;
using TickerQ.Utilities.Base;
using Xunit;

namespace ApesDb.Catalog.IntegrationTests;

public sealed class AllowedUserJobsTests
{
    [Fact]
    public void AddAsync_IsRegisteredAsTypedManualTickerFunction()
    {
        var method = typeof(AllowedUserJobs).GetMethod(nameof(AllowedUserJobs.AddAsync));

        Assert.NotNull(method);
        var functionAttribute = Assert.Single(
            method.CustomAttributes,
            value => value.AttributeType.Name == "TickerFunctionAttribute"
        );
        Assert.Equal("add-allowed-user", functionAttribute.ConstructorArguments[0].Value);
        Assert.Equal(typeof(TickerFunctionContext<AddAllowedUserRequest>), method.GetParameters()[0].ParameterType);
    }
}
