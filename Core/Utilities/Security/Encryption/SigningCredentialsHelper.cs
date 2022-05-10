using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentialsHelper(SecurityKey securityKey)
        {   //Credantials => Sisteme girmek için elinizde olanlardın mesela kullanıcı adı şife
            //burda elimizde olan bizim anahtarımız
            return  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        }
    }
}
