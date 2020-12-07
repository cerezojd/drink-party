using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DrinkParty.Jwt
{
    public class JwtTokenGenerator
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly JwtSettings _settings;


        public JwtTokenGenerator(JwtSecurityTokenHandler tokenHandler, IOptions<JwtSettings> settings)
        {
            _tokenHandler = tokenHandler;
            _settings = settings.Value;
        }

        public string GenerateJwtToken(string roomId, string playerId, string playerName)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, playerId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds.ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.GivenName, playerName),
                new Claim(JwtRegisteredClaimNames.UniqueName, playerName),
                new Claim(ClaimNames.PlayerNameClaimName, playerName),
                new Claim(ClaimNames.RoomCodeClaimName, roomId)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_settings.Expiration);

            var token = new JwtSecurityToken(
                _settings.Issuer,
                _settings.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return _tokenHandler.WriteToken(token);
        }

    }
}
