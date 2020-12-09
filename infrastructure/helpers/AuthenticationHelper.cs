using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using AlarmSystem.Core.Domain;
using System.IdentityModel.Tokens.Jwt;
using Core.Entity.DB;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace AlarmSystem.Infrastructure.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        private byte[] _secretBytes;

        public AuthenticationHelper(byte[] secretBytes)
        {
            _secretBytes = secretBytes;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
             using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            
            if(user != null){
                claims.Add(new Claim(ClaimTypes.Role, "Approved"));
            }
            
             var token = new JwtSecurityToken(
                new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(_secretBytes),
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null,
                               null,
                               claims.ToArray(),
                               DateTime.Now,
                               DateTime.Now.AddMinutes(10)));

                return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}