using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            
            RuleFor(p => p.ProductName).MinimumLength(2).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            //GreaterThanOrEqualTo büyük veya eşittir
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//unit price 10dan büyük eşit ve categori id 1 ise 

            //Olmayan kural ekleme
            RuleFor(p => p.ProductName).Must(AileBaşla).WithMessage("Ürünler A ile Başlamalı");



        }
        //arg yukarda gonderdım expression burda productName
        private bool AileBaşla(string arg)
        {
            return arg.StartsWith("A"); // A ile başlarsa true döner
        }

    }
}