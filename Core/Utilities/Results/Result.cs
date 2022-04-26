using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //bunu yapmamızın nedeni ekleme silme guncelleme işlerinde başarılı başarısız vs anlamak
        public Result(bool Succes, string Message):this(Succes) // ?????? 
        {
            message = Message;
        }
        public Result(bool Succes)
        {
            succes = Succes;
        }
        //bu yukarıdakının yerıne gene 2 ctor alttakı succes sadece ustekkı succes ve mess olucak sekılde 

        public bool succes { get; }
        public string message { get; }
      
    }
}
