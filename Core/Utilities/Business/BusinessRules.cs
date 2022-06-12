using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public IResult Run(params IResult[] Logics) //metodları gonderıyoruz burda //args kwargs mantıgı => params 
        {
            foreach (var item in Logics)
            {
                if (!item.success)
                {
                    return item; //eger problem var ise problem olan ıs parcacıgı geriye döndür
                }
            }
            return null; //eğer bir problem yoksa devam et hacı
        }
    }
}
