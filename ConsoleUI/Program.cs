using Business.Concrete;
using DataAccess.Concrete.EntitiyFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;

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

            ProductManager productManager = new ProductManager(new EfProductDal());
            //CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            //foreach (var item in categoryManager.GetAll())
            //{
            //    Console.WriteLine(item.CategoryName);
            //}
            //Console.WriteLine("///////////////////");

            //foreach (var item in productManager.GetProductDetail())
            //{
            //    Console.WriteLine(item.ProductName + "    " + item.CategoryName);
            //}

            //productManager.Add(productManager.GetAll()[0]);

            var products = productManager.GetAll();


            #region Result

            #endregion


            Console.WriteLine();
            Console.Read();
        }
    }
}
