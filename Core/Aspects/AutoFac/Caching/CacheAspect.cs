using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Aspects.AutoFac.Caching
{
    public class CacheAspect : MethodInterception
    {

        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>(); //aspect oldugu ıcın service tool kullanıyoruz klasik injection olmuyor
        }

        public override void Intercept(IInvocation invocation)
        {
            //invocation.Method => metot ismi getall gibi 
            //ReflectedType => NameSpace'i al == Business.Concrete 
            //FullName => class ismi => IProductService
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            //Business.IProductService.GetAll

            var arguments = invocation.Arguments.ToList();
            //metodun parametlerini listeye çevir

            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            //Burdaki amacımzı parametreyi yakalamak
            
            if (_cacheManager.IsAdd(key)) //eğer key var ise yani cache daha önceden olusmus buraya giricek
            {
                invocation.ReturnValue = _cacheManager.Get(key); //o zaman sen metodu hiç çalıştırmadan şimdi geri dön geri dönen değerde _cacheManager.get(key)

                return;
            }
            //eğer daha önce eklenmemi işse
            invocation.Proceed(); //metotu tamamla 
            
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //metodu tamamladıktan sonra gel buraya cache ekle
        
            //2 Senaryoda Ele alıcak olursak  1. GetAll() 2.GetByİd(2) bunlar business product'daki metotlar olsun
            //birincisinde key => Business.IProductService.GetAll()
            //İkincisinde key => Business.IProductService.GetById(2)

            //bunun sonucunda iki cache olusucak kişi get all dediğinde zaten cache oldugu için metoda girmeden cacheden direk dönücek
            //getbyid ise gene aynı mantık devam edicek fakat kişi id'si 2 olan değilde 3 olanı cagırırsa o zaman bir daha cache eklenme işlemi olucaktır
            
        
        }
    }
}
