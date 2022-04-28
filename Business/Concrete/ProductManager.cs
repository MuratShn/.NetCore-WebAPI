using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        ILogger _logger;
        //uygulama basladıgında bana bı IProductDal ver dıyo
        //Verilen IProductDal bi entitiyFramework olabılır  inmemory calısıyor olabılır artık verene kalmıs
        public ProductManager(IProductDal productDal, ILogger logger)
        {
            _productDal = productDal;
            _logger = logger;
        }

        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product entity)
        {
            ////validation Kısmı
            //ValidationTool.Validate(new ProductValidator(), entity);
            ////

            //bir ürün eklemek istersen  eklemek istediğin ürünün kat. maks 10 ürün olabilir
            //bunu burda yazmak yerine altta privated metod olusturup yazıyoruz bu sekılde kucuk iş parcacıkları olusturup dırek metotlardada kullanabılırız

            if (CheckIfProductCountOfCategoryCorrect(entity.CategoryId).succes)
            {
                Console.WriteLine("Başarılı");
                //return new SuccesResult(Messages.ProductAdded);
            }
            else
            {
                Console.WriteLine("Başarısız");
            }

            //////////////77
            ///Aynı Üründe İsim eklenemek
            if (CheckIfProductNameEqualsTheParameters(entity.ProductName).succes)
            {
                Console.WriteLine("başarılı");
            }
            else
            {
                Console.WriteLine("başarısız");
            }


            //_productDal.Add(entity);

            //return new Result(true,"Ürün Eklendi");
            return new SuccesResult();

        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(), true, Messages.ProductAdded);
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
        private IResult CheckIfProductCountOfCategoryCorrect(int Cateid)
        {
            if (GetAllByCategoryId(Cateid).Data.Count < 10)
            {
                return new SuccesResult();
            }
            return new ErrorResult();

        }
        private IResult CheckIfProductNameEqualsTheParameters(string name)
        {
            if (_productDal.GetAll(p => p.ProductName.ToLower() == name.ToLower()).Count <= 1)
            {
                return new SuccesResult();
            }
            return new ErrorResult();
        }
    }
}
