using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace RobotCloudService.Web.Components.Authentications
{
    
    
    internal static class Authentication
    {
        internal static IServiceCollection AddAuthenticationState(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBlazoredSessionStorage();
            services.AddScoped<Account>();
            services.AddSingleton(provider =>
               configuration.GetSection(Authentication.Settings.SectionName).Get<Settings>()
               ?? throw new Exception("TokenSettings is not found"));

            services.AddScoped<StateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<Authentication.StateProvider>());
            return services;
        }


        internal class Settings
        {
            public const string SectionName = "Settings:Token";
            public required string Secret { get; init; }
            public required string Issuer { get; init; }
            public required string Audience { get; init; }
            public required int ExpirationInMinutes { get; init; }
            public required bool ValidateIssuer { get; init; }
            public required bool ValidateAudience { get; init; }
            public required bool ValidateLifeTime { get; init; }
            public required bool ValidateIssuerSigningKey { get; init; }

            public SymmetricSecurityKey SymmetricSecurityKey => new(Encoding.UTF8.GetBytes(Secret));
            public SigningCredentials SigningCredentials => new(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            public TokenValidationParameters TokenValidationParameters => new()
            {
                ValidateIssuer = ValidateIssuer,
                ValidateAudience = ValidateAudience,
                ValidateLifetime = ValidateLifeTime,
                ValidateIssuerSigningKey = ValidateIssuerSigningKey,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = SymmetricSecurityKey
            };
        }

        internal class Account
        {
            
            public string UserId { get; private set; } = string.Empty;
            public string Email { get; private set; } = string.Empty;
            public string Token { get; private set; } = string.Empty;
            public bool IsAuthenticated => !string.IsNullOrEmpty(Token);


            public void Set(string userId, string email, string token)
            {
                UserId = userId;
                Email = email;
                Token = token;
            }
            public void Clear()
            {
                UserId = string.Empty;
                Email = string.Empty;
                Token = string.Empty;
            }
        }


        internal class StateProvider(Settings settings, ISessionStorageService sessionStorage, Account account) : AuthenticationStateProvider
        {

            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var handler = new JwtSecurityTokenHandler();
                var token = await sessionStorage.GetItemAsync<string>("token");

                var identity = new ClaimsIdentity();
                if (handler.CanReadToken(token))
                {
                    var readPrincipal = handler.ValidateToken(token, settings.TokenValidationParameters, out var validatedToken);

                    if (readPrincipal.Identity?.IsAuthenticated ?? false)
                    {
                        var claims = (validatedToken as JwtSecurityToken)!.Claims;
                        var email = claims.First(c => c.Type == ClaimTypes.Email).Value;
                        var userId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                        account.Set(userId, email, token);
                        identity = new ClaimsIdentity(claims, "Robot Cloud Service");
                    }
                    else
                    {
                        account.Clear();
                        await sessionStorage.RemoveItemAsync("token");
                    }
                }

                var principal = new ClaimsPrincipal(identity);
                return new AuthenticationState(principal);
            }

            public async Task NotifyUserLogIn(string token)
            {
                await sessionStorage.SetItemAsync("token", token);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }

            public async Task NotifyUserLogOut()
            {
                await sessionStorage.RemoveItemAsync("token");
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
    }




}
