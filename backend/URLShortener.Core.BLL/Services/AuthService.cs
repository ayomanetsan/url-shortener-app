using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.Common.DTO;
using URLShortener.Core.DAL.Context;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.BLL.Services
{
    public class AuthService : IAuthService
    {
        private UrlShortenerDbContext _context;
        private IConfiguration _configuration;

        public AuthService(UrlShortenerDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<UserDto> LoginAsync(LoginDto user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (dbUser != null)
            {
                if (dbUser.Password == user.Password)
                {
                    return new UserDto
                    {
                        FullName = dbUser.FirstName + " " + dbUser.LastName,
                        IsAdmin = dbUser.IsAdmin,
                        Token = GenerateAccessToken(dbUser),
                    };
                }
                else
                {
                    throw new ArgumentException("Password doesn't match");
                }
            }
            else
            {
                throw new ArgumentException("The user doesn't exist");
            }
        }

        public async Task<UserDto> RegisterAsync(User user)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (dbUser == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new UserDto
                {
                    FullName = user.FirstName + " " + user.LastName,
                    IsAdmin = false,
                    Token = GenerateAccessToken(user),
                };
            }
            else
            {
                throw new Exception("User already exists. Please try logging in instead");
            }

        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
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
