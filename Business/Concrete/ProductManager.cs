using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        //uygulama basladıgında bana bı IProductDal ver dıyo
        //Verilen IProductDal bi entitiyFramework olabılır  inmemory calısıyor olabılır artık verene kalmıs
        public ProductManager(IProductDal productDal) 
        {
            _productDal = productDal;
        }

        public IResult Add(Product entity)
        {
            if (entity.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInValid);
            }
            _productDal.Add(entity);

            //return new Result(true,"Ürün Eklendi");
            //return new SuccesResult();
            return new SuccesResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(), true ,Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccesDataResult<Product>(_productDal.Get(p => p.ProductId == id));
        }

        public IDataResult<List<Product>> GetByUnıtPrice(decimal min = 0, decimal max = 9999)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max).ToList());
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetail()
        {
            return new SuccesDataResult<List<ProductDetailDto>>(_productDal.GetProductDetail());
        }
    }
}
