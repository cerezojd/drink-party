using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkParty.Jwt
{
    public class JwtOptionsConfiguration : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtSettings _settings;

        public JwtOptionsConfiguration(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        public void PostConfigure(string name, JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key)),
                ClockSkew = TimeSpan.Zero,
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/gamehub")))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
            options.Events.OnTokenValidated += ValidateToken;

        }

        private Task ValidateToken(TokenValidatedContext ctx)
        {
            if (!(ctx.SecurityToken is JwtSecurityToken jwtToken)
             || jwtToken.SignatureAlgorithm != SecurityAlgorithms.HmacSha256
             || jwtToken.Claims.All(x => x.Type != JwtRegisteredClaimNames.Iat)
         )
            {
                ctx.Fail("Invalid JWT Token");
                return Task.CompletedTask;
            }

            if (DateTime.Now < jwtToken.ValidFrom || DateTime.Now > jwtToken.ValidTo)
            {
                ctx.Fail("JWT Token Expired");
                return Task.CompletedTask;
            }
            
            ctx.Success();
            return Task.CompletedTask;

        }
    }
}
