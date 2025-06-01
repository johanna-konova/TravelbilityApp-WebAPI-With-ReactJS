using StackExchange.Redis;

using TravelbilityApp.WebAPI.Contracts;

namespace TravelbilityApp.WebAPI.Services
{
    public class RedisTokenStore(IConnectionMultiplexer multiplexer) : ITokenStore
    {
        private readonly IDatabase database= multiplexer.GetDatabase();

        public Task StoreRefreshTokenAsync(string token, TimeSpan ttl, string userId)
            => database.StringSetAsync(token, userId, ttl);

        public async Task<bool> ValidateRefreshTokenAsync(string token, string userId)
        {
            var storedUser = await database
                .StringGetAsync(token)
                .ConfigureAwait(false);

            if (storedUser.IsNullOrEmpty)
            {
                return false;
            }

            return storedUser == userId;
        }

        public Task RevokeRefreshTokenAsync(string token)
            => database.KeyDeleteAsync(token);

        public Task StoreAccessTokenAsync(string jti, TimeSpan ttl)
            => database.StringSetAsync($"access_{jti}", "valid", ttl);

        public async Task<bool> ValidateAccessTokenAsync(string jti)
        {
            var entry = await database
                .StringGetAsync($"access_{jti}")
                .ConfigureAwait(false);

            return entry.IsNullOrEmpty == false;
        }

        public Task RevokeAccessTokenAsync(string jti)
            => database.KeyDeleteAsync($"access_{jti}");
    }
}
