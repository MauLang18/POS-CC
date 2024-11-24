using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using POS.Application.Commons.Config;
using POS.Application.Interfaces.Services;
using POS.Infrastructure.Authentication;
using System.Text;

namespace POS.Api.Middleware;

public static class AuthenticationExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var vaultSecretService = serviceProvider.GetRequiredService<IVaultSecretService>();

        var secretJson = vaultSecretService.GetSecret("CustomCodeAPI/data/Jwt").GetAwaiter().GetResult();
        var secretResponse = JsonConvert.DeserializeObject<SecretResponse<JwtSettings>>(secretJson);

        if (secretResponse?.Data?.Data == null)
        {
            throw new Exception("Failed to retrieve secrets from Vault.");
        }

        var jwtSettings = secretResponse.Data.Data;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        return services;
    }
}