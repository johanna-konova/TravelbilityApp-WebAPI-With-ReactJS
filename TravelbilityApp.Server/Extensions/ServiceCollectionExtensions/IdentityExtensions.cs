using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using TravelbilityApp.Infrastructure.Data;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.WebAPI.Contracts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddApplicationIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount =
                        configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                    options.Password.RequireDigit =
                        configuration.GetValue<bool>("Identity:Password:RequireDigit");
                    options.Password.RequireLowercase =
                        configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                    options.Password.RequireNonAlphanumeric =
                        configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                    options.Password.RequireUppercase =
                        configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                    options.Password.RequiredLength =
                        configuration.GetValue<int>("Identity:Password:RequiredLength");
                })
                .AddEntityFrameworkStores<TravelbilityAppDbContext>()
                .AddDefaultTokenProviders();

            services
              .AddAuthentication(options => {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(options => {
                  options.RequireHttpsMetadata = true;
                  options.SaveToken = true;

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidIssuer = configuration["Jwt:Issuer"],

                      ValidateAudience = true,
                      ValidAudience = configuration["Jwt:Audience"],

                      ValidateLifetime = true,

                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                      ValidateIssuerSigningKey = true
                  };

                  options.Events = new JwtBearerEvents
                  {
                      OnTokenValidated = async context =>
                      {
                          var jti = context.Principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

                          if (string.IsNullOrEmpty(jti))
                          {
                              context.Fail("Token does not contain a valid jti.");
                              return;
                          }

                          var tokenStore = context.HttpContext.RequestServices.GetRequiredService<ITokenStore>();

                          if (await tokenStore.ValidateAccessTokenAsync(jti) == false)
                          {
                              context.Fail("Token has been revoked or expired.");
                          }
                      }
                  };
              });

            services.AddAuthorization();

            return services;
        }
    }
}
