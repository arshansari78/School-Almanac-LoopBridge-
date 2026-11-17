using LoopBridge.ViewModels.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace LoopBridge.Api.Common
{
    public static class JwtSettings
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var configration = builder.Configuration;
            var jwtSection = configration.GetSection("Jwt");
            builder.Services.Configure<JwtConfigVm>(jwtSection);

            JwtConfigVm jwtConfig = jwtSection.Get<JwtConfigVm>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Key);

            builder.Services.AddAuthentication(e =>
            {
                e.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                e.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(e =>
            {
                e.RequireHttpsMetadata = true;
                e.SaveToken = true;
                e.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
        }
        public static void OpenApiSetting(WebApplicationBuilder builder)
        {
            builder.Services.AddOpenApi(o =>
            {
                o.AddDocumentTransformer((document, _, _) =>
                {
                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes.Add(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        BearerFormat = JwtBearerDefaults.AuthenticationScheme,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Type = SecuritySchemeType.Http
                    });
                    return Task.CompletedTask;
                });
            });
        }
    }
}
