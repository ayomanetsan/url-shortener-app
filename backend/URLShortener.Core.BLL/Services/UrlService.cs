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
            var dbUrl = await _context.Urls.FirstOrDefaultAsync(u => u.Id == id);

            if (dbUrl == null)
            {
                throw new ArgumentException("URL with such id does not exist");
            }

            return dbUrl;
        }

        public async Task<ICollection<Url>> GetUrls()
        {
            return await _context.Urls.ToListAsync();
        }

        public async Task<string> ShortenUrl(string url)
        {
            var dbUrl = _context.Urls.FirstOrDefault(u => u.OriginalUrl == url);

            if (dbUrl == null)
            {
                var maxId = await _context.Urls.MaxAsync(u => u.Id) + 1;

                dbUrl = new Url { };
                dbUrl.CreatedBy = new User { };
                dbUrl.OriginalUrl = url;
                dbUrl.ShortUrl = Encode(maxId);

                _context.Urls.Add(dbUrl);
                await _context.SaveChangesAsync();
            }

            return dbUrl.ShortUrl;
        }

        private string Encode(uint id)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var sb = new StringBuilder();
            while (id > 0)
            {
                sb.Append(chars[(int)id % 62]);
                id /= 62;
            }

            return new string(sb.ToString().Reverse().ToArray());
        }
    }
}
