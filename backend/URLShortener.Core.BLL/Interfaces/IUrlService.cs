using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Core.DAL.Entitites;

namespace URLShortener.Core.BLL.Interfaces
{
    public interface IUrlService
    {
        Task<Url> GetUrl(uint id);

        Task<ICollection<Url>> GetUrls();

        Task<Url> ShortenUrl(string url, string email);

        Task<ICollection<Url>> DeleteUrl(string shortUrl);

        Task<string> Redirect(string shortUrl);

        Task<string> SetDescription(string description)
    }
}
