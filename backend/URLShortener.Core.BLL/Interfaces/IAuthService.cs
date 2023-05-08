using URLShortener.Core.Common.DTO;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(LoginDto user);

        Task<UserDto> RegisterAsync(User user);
    }
}
