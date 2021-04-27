using DataService.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private static readonly string _secret = Guid.NewGuid().ToString();
        public static byte[] Key => Encoding.ASCII.GetBytes(_secret);

        private readonly IUsersService _service;

        public AuthService(IUsersService service)
        {
            _service = service;
        }

        public string Authenticate(string login, string password)
        {
            UserRoles? roles = _service.GetRoles(login, password);
            if (roles == null || roles == 0)
            {
                return null;
            }

            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login),
                };
            claims.AddRange(roles.Value.ToString().Split(",").Select(x => new Claim(ClaimTypes.Role, x.Trim())));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
