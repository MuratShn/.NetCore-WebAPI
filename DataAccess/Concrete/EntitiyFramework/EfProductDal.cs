using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitiyFramework
{
    //Product Dalı Neden sılmıyoruzun sebebı ise ilerde product ile özel işlemleri yapacağımız zaman oraya yazıcak olmamış
    public class EfProductDal : EfEntityRepositoryBase<Product, NortwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetail()
        {
            using (NortwindContext context = new NortwindContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 CategoryName = c.CategoryName,
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
