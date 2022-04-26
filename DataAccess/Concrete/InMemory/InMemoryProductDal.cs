using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>()
            {
                new(){CategoryId = 1, ProductId = 1, ProductName = "product1", UnitPrice = 100, UnitsInStock = 10 },
                new(){CategoryId = 1, ProductId = 2, ProductName = "product2", UnitPrice = 200, UnitsInStock = 20 },
                new(){CategoryId = 2, ProductId = 3, ProductName = "product3", UnitPrice = 300, UnitsInStock = 30 },
                new(){CategoryId = 2, ProductId = 4, ProductName = "product4", UnitPrice = 400, UnitsInStock = 40 },
                new(){CategoryId = 2, ProductId = 5, ProductName = "product5", UnitPrice = 500, UnitsInStock = 50 },
                new(){CategoryId = 1, ProductId = 6, ProductName = "product6", UnitPrice = 600, UnitsInStock = 60 },
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product DeletedProduct = null;
            DeletedProduct = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            _products.Remove(DeletedProduct);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetail()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
