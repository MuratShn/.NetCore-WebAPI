using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //bunun classın maksatı ICategoryDal IProductDal her seferınde bu temel metodları tanımlamamak onlara mıras vermek burdakı t ıse product mı 
    //categorymı oldugunu belırlıyor

    //Generic Constraint generic kısıt demek gelicek T'nin ne oldugunu hasbel kadar bellı edıyor
    // Class olabılır demek degıl referans tip olabilir dmeek
    public interface IEntitiyRepository<T> where T:class,IEntitiy
    {

        //GetAll kısmında ben bütün veriler gelsin istiyorum ama filtrede uygulamak ıstıyorum aynı anda sundan buyuklerın hepsini getir gibi
        //bu sekılde fıltre vermemızı saglayan yapının ısmı Expression
        List<T> GetAll(Expression<Func<T,bool>> filter = null); //filter = null filtre vermeyebilirsin
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entitiy);
        void Update(T entitiy);
        void Delete(T entitiy);

        List<T> GetByCategory(int categoryId); //ürünlei kategoriye göre filtrtleme
    }
}