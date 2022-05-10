using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        //Token Üreticek Mekanızmanın Kendini kim için ve hangi yetkileri olucak

        //Senaryo => Bir kullanıcı adı şifresini doğru yazdıgında buraya geldi ve tokenini aldı yetkilerine uygun bir biçimde
        AccesToken CreateToken(User user, List<OperationClaim> operationClaims);

    }
}
