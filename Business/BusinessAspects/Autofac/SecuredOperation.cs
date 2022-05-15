using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{

    //JWT İÇİN BURASI
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;

        //Kullanıcıdan Alınan her istek için http contexi olusur
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles) //Burda Rolleri Alıyor şu sekılde metotların ustune yazıyoruz => [SecuredOperation("Moderator,Admin")]
        {
            _roles = roles.Split(','); //Rolleri ayırıyoruz


            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

            //ServiceTool'u biz yazdık bizim autofac ile olusturdugumuz service mimarisine ulaş 
            //Aspect oldugu ıcın ınject edemıyoruz onu 

            //Senaryo => Bizde ProductService var Onu IProductService'e ınject ediyoruz ||
            //productService = ServiceTool.ServiceProvider.GetService<IProductService>();

        }

        //Kullanılıcak metodun onunde calıstırdık demek mesela Add metonun ustunde [SecuredOperation("..")] yazdıgımızda bura calıscak
        //Bu MetotInterceptiondan geliyor
        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); //Rollerini buluyoruz
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) //claimlerinin içinde ilgili rol varsa return et
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied); //egerkı metot yok ise hata ver
        }
    }
}

