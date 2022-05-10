using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public class AccesToken 
    {
        public string Token { get; set; } // adamın Tokenı
        public DateTime Expiration { get; set; } //Ne zamana kadar geçerliği vardor
    }
}
