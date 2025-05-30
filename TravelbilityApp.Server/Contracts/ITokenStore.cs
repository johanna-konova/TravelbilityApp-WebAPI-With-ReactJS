namespace TravelbilityApp.WebAPI.Contracts
{
    public interface ITokenStore
    {
        Task StoreRefreshTokenAsync(string token, TimeSpan ttl, string userId);
        Task<bool> ValidateRefreshTokenAsync(string token, string userId);
        Task RevokeRefreshTokenAsync(string token);
    }
}
