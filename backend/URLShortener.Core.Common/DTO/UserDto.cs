using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Core.Common.DTO
{
    public class UserDto
    {
        public string FullName {  get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
