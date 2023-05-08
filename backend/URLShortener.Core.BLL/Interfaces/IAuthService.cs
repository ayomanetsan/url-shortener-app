using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.Common.DTO;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> LoginAsync(UserDto user);

        Task<UserDto> RegisterAsync(User user);

        string GenerateAccessToken(User user);
    }
}
