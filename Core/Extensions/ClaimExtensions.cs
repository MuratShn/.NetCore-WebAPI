using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        //EXTENSİON == GENİŞLETMEK
        //var olan bir nesneye metot ekleyebılıyoruz
        //Yukarıdakı claim sınıfı system olan bir sinif onda addname vs gibi metotlar yok onları biz kendimiz yazdık(core->Extensionda)
        //Extension metod yazabilmek için hem classın hem metodın statik olması gerekiyor


        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            //Bu metod(AddEmail) Sunun(This ICollection<Claim> claims) ıcıne eklenıcek
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            //string dizisi olan rolleri listeye cevir her ve herbiri claim'e ekle
            //foreach (var item in roles.ToList())
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, item);
            //}
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
    }
}
