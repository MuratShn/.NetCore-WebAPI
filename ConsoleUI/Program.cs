using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntitiyFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region InMemortDal

            //ProductManager productManager = new ProductManager(new InMemoryProductDal());
            //var products = productManager.GetAll();

            //products.Any(p=>p.ProductName=="acer"); //product name acer olan eleman varmı yokmu true false dondurucek
            //var a = products.Find(p=>p.CategoryId==2); //product name acer olan eleman varmı yokmu donecek ama direk ürünü eger birden fazla bulursa ılk buldugunu alıyor
            //var b = products.FindAll(p=>p.CategoryId==2); //product name acer olan eleman varmı yokmu donecek ama direk ürünü 

            //Console.WriteLine(a.ProductName);
            //Console.WriteLine(b.Count);

            #endregion

            #region ProductTest

            //ProductManager productManager = new ProductManager(new EfProductDal());
            //foreach (var item in productManager.GetAll())
            //{
            //    Console.WriteLine(item.ProductName);
            //}
            //Console.WriteLine("---------------------");
            //foreach (var item in productManager.GetByUnıtPrice(max: 20))
            //{
            //    Console.WriteLine(item.ProductName);
            //}
            //Console.WriteLine("-------------");
            //foreach (var item in productManager.GetAllByCategoryId(2))
            //{
            //    Console.WriteLine(item.ProductName);
            //}
            #endregion

            //ProductManager productManager = new ProductManager(new EfProductDal());
            ////CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            ////foreach (var item in categoryManager.GetAll())
            ////{
            ////    Console.WriteLine(item.CategoryName);
            ////}
            ////Console.WriteLine("///////////////////");

            ////foreach (var item in productManager.GetProductDetail())
            ////{
            ////    Console.WriteLine(item.ProductName + "    " + item.CategoryName);
            ////}

            ////productManager.Add(productManager.GetAll()[0]);

            //var products = productManager.GetAll();


            //#region Result

            //#endregion


            #region JWT TEST Hashleme sıkıntısı

            //string sifre = "12345";

            //byte[] passwordSalt, passwordHash;


            //using (var hmac = new HMACSHA512()) //burda hazır algolaritmaarı kullancaz
            //{
            //    passwordSalt = hmac.Key;
            //    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre)); //parametre olarak sifreyi veriyoz ama byte seklınde ıstedıgı ıcın donusme yapıyoruz

            //}


            //sifre = "ts123";


            //var _userService = new UserManager(new EfUserDal());

            //_userService.Add(new User()
            //{
            //    Email = sifre,

            //    FirstName = passwordHash.ToString(),
            //    LastName = passwordSalt.ToString(),

            //    PasswordHash = passwordHash,
            //    PasswordSalt = passwordSalt

            //});


            /////////////////// Şifreleme kısmı yukarıdaydı şimdi kontrol

            //var girilensifre = "12345";

            //var user2 = _userService.GetUserByMail(sifre); //yukarıdakı veriyi almış olucam böyle


            //using (var hmac = new HMACSHA512(user2.Data.PasswordSalt)) 
            //{
            //    passwordSalt = hmac.Key;
                
            //    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(girilensifre)); //parametre olarak sifreyi veriyoz ama byte seklınde ıstedıgı ıcın donusme yapıyoruz

            //    var elimdeki = user2.Data.PasswordHash;

            //}




            #endregion

            Console.Read();
        }
    }
}
