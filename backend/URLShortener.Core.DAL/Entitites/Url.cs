using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Core.DAL.Entitites
{
    public class Url
    {
        public uint Id { get; set; }
        public string ShortUrl { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
        public User CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
