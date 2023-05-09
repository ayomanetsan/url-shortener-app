using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Core.DAL.Entitites
{
    public class Description
    {
        public uint Id { get; set; }
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
