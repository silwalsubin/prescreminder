using Microsoft.IdentityModel.Tokens;
using prescreminder.Utilities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace middleware.Authentication
{
    public class AuthenticationMiddleWare
    {
        public static string GenerateJsonWebToken(Claim[] claims = null)
        {
            var authenticationSettings = AppSettingsUtility.GetSettings<AuthenticationSettings>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: DateTime.Now.AddMinutes(authenticationSettings.TokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}