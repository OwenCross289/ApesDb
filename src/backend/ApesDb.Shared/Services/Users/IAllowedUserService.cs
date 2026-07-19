namespace ApesDb.Shared.Services.Users;

public interface IAllowedUserService
{
    Task<bool> IsAllowedAsync(string? email, CancellationToken cancellationToken = default);

    Task<bool> AddAsync(string email, CancellationToken cancellationToken = default);
}
