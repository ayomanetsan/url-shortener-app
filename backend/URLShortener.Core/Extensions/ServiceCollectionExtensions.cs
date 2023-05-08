using URLShortener.Core.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace URLShortener.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUrlShortenerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("UrlShortenerDbConnection");
            services.AddDbContext<UrlShortenerDbContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(UrlShortenerDbContext).Assembly.GetName().Name)));
        }
    }
}
