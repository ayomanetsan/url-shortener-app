using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.BLL.Services;
using URLShortener.Core.Common.DTO;
using URLShortener.Core.DAL.Context;
using URLShortener.Core.DAL.Entitites;

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

        [Fact]
        public async Task LoginAsyncWithValidCredentialsReturnsUserDto()
        {
            var user = new User { 
                FirstName = "Test", 
                LastName = "User", 
                Email = "testuser@test.com", 
                Password = "password" 
            };

            using (var context = new UrlShortenerDbContext(_options))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            var loginDto = new LoginDto { Email = "testuser@test.com", Password = "password" };
            var expected = await _sut.LoginAsync(loginDto);

            Assert.NotNull(expected);
            Assert.Equal("Test User", expected.FullName);
            Assert.False(expected.IsAdmin);
            Assert.NotNull(expected.Token);
        }
    }
}