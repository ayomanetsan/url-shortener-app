using Microsoft.EntityFrameworkCore;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.DAL.Context
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
