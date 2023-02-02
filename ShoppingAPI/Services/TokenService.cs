using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShoppingAPI.Services.Contracts;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.Models;

namespace ShoppingAPI.Services
{
	public class TokenService : ITokenService
    {
        private readonly IConfiguration _iconfiguration;
        
        public TokenService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public string GenerateJwt(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claims = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role!.Name!)
                  }

            );

            var key = Encoding.ASCII.GetBytes(_iconfiguration["JWT:Key"]!);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            string jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}

