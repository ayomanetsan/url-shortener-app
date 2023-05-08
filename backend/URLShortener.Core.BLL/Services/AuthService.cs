using Microsoft.EntityFrameworkCore;
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
            throw new NotImplementedException();        
        }

        public string GenerateAccessToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
