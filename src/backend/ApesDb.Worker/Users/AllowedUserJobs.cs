using ApesDb.Shared.Services.Users;
using TickerQ.Utilities.Base;

namespace ApesDb.Worker.Users;

public sealed class AllowedUserJobs
{
    private readonly IAllowedUserService _allowedUserService;

    public AllowedUserJobs(IAllowedUserService allowedUserService)
    {
        _allowedUserService = allowedUserService;
    }

    [TickerFunction(AllowedUserFunctions.Add, maxConcurrency: 1)]
    public async Task AddAsync(
        TickerFunctionContext<AddAllowedUserRequest> context,
        CancellationToken cancellationToken
    )
    {
        await _allowedUserService.AddAsync(context.Request.Email, cancellationToken);
    }
}
