using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Interceptors;
using Castle.DynamicProxy;

namespace Core.Aspects.AutoFac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        //Data Bozuldugu zaman calısır(data eklendıgınde sılındıgıne..) 
        //Bir Serviste(Manager'da) veriyi manipule(crud işlemleri gibi) eden metotladlarına cache remove aspect uygulanır

        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        //kullanıcı yeni bir product eklemek isterse ve ve hata alırsa(validation yada buss. kodlarından) cache'deki veriyi boşuna silmiş oluruz
        //bunun önüne geçmek içinde OnSuccess kullanıyoruz yani metot başarılı şekilde çalışırsa uygula gibisinden
        protected override void OnSuccess(IInvocation invocation) 
        {
            _cacheManager.RemovePattern(_pattern);
        }

    }
}
