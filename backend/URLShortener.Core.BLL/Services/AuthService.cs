using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.Common.DTO;
using URLShortener.Core.DAL.Context;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.BLL.Services
{
    public class AuthService : IAuthService
    {
        private UrlShortenerDbContext _context;

        public AuthService(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public Task<UserDto> LoginAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> RegisterAsync(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (dbUser == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var token = GenerateAccessToken(user);

                return new UserDto
                {
                    FullName = user.FirstName + " " + user.LastName,
                    IsAdmin = false,
                    Token = token,
                };
            }
            else
            {
                throw new Exception("User already exists. Please try logging in instead.");
            }
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("H@McQfTjWnZr4u7w");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
