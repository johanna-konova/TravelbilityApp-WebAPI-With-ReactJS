using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using TravelbilityApp.Core.DTOs.Account;
using TravelbilityApp.Infrastructure.Data.Models;
using TravelbilityApp.WebAPI.Contracts;

using static TravelbilityApp.Core.Constants.MessagesConstants;

namespace TravelbilityApp.WebAPI.Controllers
{
    [Authorize]
    public class AccountController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ITokenStore tokenStore) : BaseController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var isUserExisting = await userManager.FindByEmailAsync(dto.Email);

            if (isUserExisting != null)
            {
                ModelState.AddModelError("Email", AlreadyUsedEmail);
                return Conflict(ModelState);
            }

            var user = new ApplicationUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);
            }

            bool isCurrentLoggedInUserAdmin = await IsCurrentLoggedInUserAdmin(user);

            var (token, expires, refreshToken) = await GenerateTokensAsync(user, isCurrentLoggedInUserAdmin);

            var responseDto = ConstructResponseDto(user, token, expires, refreshToken, isCurrentLoggedInUserAdmin);

            return Created(string.Empty, responseDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginDto dto)

        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user == null ||
                await userManager.CheckPasswordAsync(user, dto.Password) == false)
            {
                return Unauthorized(new { Message = InvalidCredentials });
            }

            bool isCurrentLoggedInUserAdmin = await IsCurrentLoggedInUserAdmin(user);

            var (token, expires, refreshToken) = await GenerateTokensAsync(user, isCurrentLoggedInUserAdmin);

            var responseDto = ConstructResponseDto(user, token, expires, refreshToken, isCurrentLoggedInUserAdmin);

            return Ok(responseDto);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh(RefreshRequestDto model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            
            var user = await userManager.FindByIdAsync(model.UserId.ToString());
            
            if (user == null)
            {
                return Unauthorized(new { Message = InvalidRefreshToken });
            }

            if (!await tokenStore.ValidateRefreshTokenAsync(model.RefreshToken, model.UserId.ToString()))
            {
                return Unauthorized(new { Message = InvalidOrExpiredRefreshToken });
            }
                
            await tokenStore.RevokeRefreshTokenAsync(model.RefreshToken);

            bool isCurrentLoggedInUserAdmin = await IsCurrentLoggedInUserAdmin(user);

            var (token, expires, refreshToken) = await GenerateTokensAsync(user, isCurrentLoggedInUserAdmin);

            var responseDto = ConstructResponseDto(user, token, expires, refreshToken, isCurrentLoggedInUserAdmin);

            return Ok(responseDto);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout(LogoutRequestDto dto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                await tokenStore.RevokeRefreshTokenAsync(dto.RefreshToken);
            }

            var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (string.IsNullOrEmpty(jti) == false)
            {
                await tokenStore.RevokeAccessTokenAsync(jti);
            }

            return NoContent();
        }

        private async Task<(string token, DateTime expires, string refreshToken)> GenerateTokensAsync(
            ApplicationUser user,
            bool isCurrentLoggedInUserAdmin)
        {
            var (token, jti, expires) = GenerateJwtToken(user, isCurrentLoggedInUserAdmin);

            var ttlAccess = expires - DateTime.UtcNow;
            await tokenStore.StoreAccessTokenAsync(jti, ttlAccess);

            var refreshToken = GenerateRefreshToken();
            var refreshTtlDays = int.Parse(configuration["Jwt:RefreshTokenDurationInDays"] ?? "7");

            await tokenStore.StoreRefreshTokenAsync(refreshToken, TimeSpan.FromDays(refreshTtlDays), user.Id.ToString());

            return (token, expires, refreshToken);
        }

        private (string token, string jti, DateTime expires) GenerateJwtToken(
            ApplicationUser user,
            bool isCurrentLoggedInUserAdmin)
        {
            var jti = Guid.NewGuid().ToString();

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim("uid", user.Id.ToString()),
                new Claim("IsAdmin", isCurrentLoggedInUserAdmin.ToString()),
            };

            var keyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
            var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMonths(1);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: authClaims,
                expires: expires,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return (token, jti, expires);
        }

        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        private async Task<bool> IsCurrentLoggedInUserAdmin(ApplicationUser user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            return userRoles.Contains("Administrator");
        }

        private ResponseDto ConstructResponseDto(
            ApplicationUser user,
            string token,
            DateTime expires,
            string refreshToken,
            bool isCurrentLoggedInUserAdmin)
            => new ResponseDto()
            {
                Id = user.Id,
                Email = user.Email!,
                AccessToken = token,
                Expires = expires,
                RefreshToken = refreshToken,
                IsAdmin = isCurrentLoggedInUserAdmin
            };
    }
}
