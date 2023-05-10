using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.BLL.Services;
using URLShortener.Core.DAL.Context;

namespace URLShortener.Tests
{
    public class UrlServiceTests
    {
        private readonly DbContextOptions<UrlShortenerDbContext> _options;
        private readonly IAuthService _authService;
        private readonly IUrlService _sut;
        private readonly IConfigurationRoot _configuration;

        public UrlServiceTests()
        {
            _options = new DbContextOptionsBuilder<UrlShortenerDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                { "Jwt:SecretKey", "t7w!z%C*F-JaNdRg" },
                { "Jwt:Issuer", "localhost" },
                { "Jwt:Audience", "localhost" }
                })
                .Build();

            var sharedContext = new UrlShortenerDbContext(_options);

            _authService = new AuthService(sharedContext, _configuration);
            _sut = new UrlService(sharedContext);
        }
    }
}
