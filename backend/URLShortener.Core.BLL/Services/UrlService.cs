using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.BLL.Interfaces;
using URLShortener.Core.DAL.Context;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.BLL.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerDbContext _context;

        public UrlService(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public async Task<Url> GetUrl(uint id)
        {
            var dbUrl = await _context.Urls.Include(u => u.CreatedBy).FirstOrDefaultAsync(u => u.Id == id);

            if (dbUrl == null)
            {
                throw new ArgumentException("URL with such id does not exist");
            }

            return dbUrl;
        }

        public async Task<ICollection<Url>> GetUrls()
        {
            return await _context.Urls.Include(u => u.CreatedBy).ToListAsync();
        }

        public async Task<Url> ShortenUrl(string url, string email)
        {
            var dbUrl = _context.Urls.FirstOrDefault(u => u.OriginalUrl == url);

            if (dbUrl == null)
            {
                dbUrl = new Url { };
                dbUrl.CreatedBy = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                dbUrl.CreatedAt = DateTime.Now;
                dbUrl.OriginalUrl = url;
                var id = Guid.NewGuid().ToString("N").Substring(0, 8);
                dbUrl.ShortUrl = Encode(id);

                _context.Urls.Add(dbUrl);
                await _context.SaveChangesAsync();

                return dbUrl;
            }

            throw new ArgumentException("This URL already exists");
        }

        public async Task<ICollection<Url>> DeleteUrl(string shortUrl)
        {
            var dbUrl = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);

            if (dbUrl != null)
            {
                _context.Remove(dbUrl);
                await _context.SaveChangesAsync();
                return await _context.Urls.ToListAsync();
            }

            throw new ArgumentException("URL with such code does not exist");
        }

        public async Task<string> Redirect(string shortUrl)
        {
            var dbUrl = await _context.Urls.FirstOrDefaultAsync(u => u.ShortUrl == shortUrl);

            if (dbUrl != null)
            {
                return dbUrl.OriginalUrl;
            }

            throw new ArgumentException("URL with such code does not exist");
        }

        public async Task<string> SetDescription(string description)
        {
            var newDescription = new Description
            {
                Content = description,
                UpdatedAt = DateTime.Now,
            };

            _context.Description.Add(newDescription);
            await _context.SaveChangesAsync();

            return newDescription.Content;
        }

        private string Encode(string id)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var sb = new StringBuilder();
            for (int i = 0; i < id.Length; i += 2)
            {
                var chunk = id.Substring(i, 2);
                var index = Convert.ToInt32(chunk, 16) % 62;
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}
