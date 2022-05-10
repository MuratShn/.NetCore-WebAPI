using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper //Hash OOlusturmaya ve onu dogrulamaya yarıyor
    {
        public static void CreatePasswordHash
            (string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            //out dısarıya cıkıca olan 
            using (var hmac = new HMACSHA512()) //burda hazır algolaritmaarı kullancaz
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //parametre olarak sifreyi veriyoz ama byte seklınde ıstedıgı ıcın donusme yapıyoruz

            }

        }
        public static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                } //Nesne referans olayından dolayı boyle yaptık
                
                return true;
            }
           
        }
    }
}
