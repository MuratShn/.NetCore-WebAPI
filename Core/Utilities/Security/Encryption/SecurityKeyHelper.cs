using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper //encrypton'a parametre geçmek için byte araya cevırmemız lazım strıngı ona cevıyoro ? ve onu bir simetrik
        //anahtar haline getiriyor
    {

        public static SecurityKey CreateSecuritykey(string securtiyKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securtiyKey))
        }
    }
}
