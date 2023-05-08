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
            throw new NotImplementedException();        
        }

        public string GenerateAccessToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
