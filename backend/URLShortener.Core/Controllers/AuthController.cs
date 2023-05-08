using Microsoft.AspNetCore.Mvc;
using URLShortener.Core.BLL.Interfaces;

namespace URLShortener.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
    }
}