using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

using TravelbilityApp.Core.Contracts;
using TravelbilityApp.Core.Services;
using TravelbilityApp.Infrastructure.Common;
using TravelbilityApp.Infrastructure.Data;
using TravelbilityApp.WebAPI.Contracts;
using TravelbilityApp.WebAPI.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]));

            services.AddScoped<ITokenStore, RedisTokenStore>();

            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<IFacilityService, FacilityService>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<TravelbilityAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
