using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.BLL.Services;
using URLShortener.Core.DAL.Context;

namespace URLShortener.Tests
{
    public class AuthServiceTests
    {
        private readonly DbContextOptions<UrlShortenerDbContext> _options;
        private readonly IAuthService _sut;
        private readonly IConfigurationRoot _configuration;

        public AuthServiceTests()
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

            _sut = new AuthService(new UrlShortenerDbContext(_options), _configuration);
        }
    }
}