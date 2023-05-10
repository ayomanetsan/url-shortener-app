using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.BLL.Services;
using URLShortener.Core.DAL.Context;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Tests
{
    public class UrlServiceTests
    {
        private readonly DbContextOptions<UrlShortenerDbContext> _options;
        private IUrlService _sut;
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

            _sut = new UrlService(new UrlShortenerDbContext(_options));
        }

        [Fact]
        public async Task GetUrlExistingUrlReturnsUrl()
        {
            var creator = new User
            {
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@test.com",
                Password = "password",
            };

            var url = new Url { 
                Id = 1,
                OriginalUrl = "http://example.com", 
                ShortUrl = "abc123" ,
                CreatedBy = creator,
            };

            using (var context = new UrlShortenerDbContext(_options))
            {
                context.Users.Add(creator);
                context.Urls.Add(url);
                await context.SaveChangesAsync();
            }

            var expected = await _sut.GetUrl(1);

            Assert.NotNull(expected);
            Assert.Equal((uint)1, expected.Id);
            Assert.Equal("http://example.com", expected.OriginalUrl);
            Assert.Equal("abc123", expected.ShortUrl);
        }

        [Fact]
        public async Task GetUrlNonExistingUrlThrowsArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetUrl(0));
        }

        [Fact]
        public async Task GetUrlsReturnsAllUrls()
        {
            var creator = new User
            {
                FirstName = "Test",
                LastName = "Creator",
                Email = "testcreator@test.com",
                Password = "password",
            };

            var urls = new List<Url>
            {
                new Url { Id = 10, OriginalUrl = "http://example.com", ShortUrl = "abc123", CreatedBy = creator },
                new Url { Id = 11, OriginalUrl = "http://example.org", ShortUrl = "xyz789", CreatedBy = creator }
            };

            using (var context = new UrlShortenerDbContext(_options))
            {
                context.Users.Add(creator);
                context.Urls.AddRange(urls);
                await context.SaveChangesAsync();
            }

            var expected = await _sut.GetUrls();

            Assert.NotNull(expected);
            Assert.Equal(2, expected.Count);
            Assert.Contains(expected, u => u.Id == 10 && u.OriginalUrl == "http://example.com" && u.ShortUrl == "abc123");
            Assert.Contains(expected, u => u.Id == 11 && u.OriginalUrl == "http://example.org" && u.ShortUrl == "xyz789");
        }

        [Fact]
        public async Task ShortenUrlNewUrlReturnsShortenedUrl()
        {
            var expected = await _sut.ShortenUrl("http://abc123.com", "testuser@test.com");

            Assert.NotNull(expected);
            Assert.Equal("http://abc123.com", expected.OriginalUrl);
            Assert.NotNull(expected.ShortUrl);
            Assert.Equal("testuser@test.com", expected.CreatedBy.Email);
        }

        [Fact]
        public async Task ShortenUrlExistingUrlThrowsArgumentException()
        {
            var firstUrl = await _sut.ShortenUrl("http://test.com", "testuser@test.com");

            await Assert.ThrowsAsync<ArgumentException>(() => _sut.ShortenUrl("http://test.com", "testuser@test.com"));
        }
    }
}
