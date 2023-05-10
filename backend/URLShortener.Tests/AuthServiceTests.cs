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

        [Fact]
        public async Task LoginAsyncWithInvalidPasswordThrowsArgumentException()
        {
            var user = new User
            {
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

            var loginDto = new LoginDto { Email = "testuser@test.com", Password = "wrong" };

            await Assert.ThrowsAsync<ArgumentException>(() => _sut.LoginAsync(loginDto));
        }

        [Fact]
        public async Task LoginAsyncWithNonExistingCredentialsThrowsArgumentException()
        {
            var loginDto = new LoginDto { Email = "testuser@test.com", Password = "password" };

            await Assert.ThrowsAsync<ArgumentException>(() => _sut.LoginAsync(loginDto));
        }

        [Fact]
        public async Task RegisterAsyncWithValidCredentialsReturnsUserDto()
        {
            var user = new User
            {
                FirstName = "New",
                LastName = "User",
                Email = "newuser@test.com",
                Password = "password"
            };

            var expected = await _sut.RegisterAsync(user);

            Assert.NotNull(expected);
            Assert.Equal("New User", expected.FullName);
            Assert.False(expected.IsAdmin);
            Assert.NotNull(expected.Token);
        }

        [Fact]
        public async Task RegisterAsyncWithExistingUserThrowsArgumentException()
        {
            var user1 = new User
            {
                FirstName = "New",
                LastName = "User",
                Email = "newuser1@test.com",
                Password = "password"
            };

            var user2 = new User
            {
                FirstName = "New",
                LastName = "User",
                Email = "newuser1@test.com",
                Password = "password"
            };

            await _sut.RegisterAsync(user1);

            await Assert.ThrowsAsync<Exception>(() => _sut.RegisterAsync(user2));
        }
    }
}