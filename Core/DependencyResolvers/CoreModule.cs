using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //Buratı Startup olarak dusunebılırız

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //her istekte olusan context baslangıctan bitişe kadar request response arası
            
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            //serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>(); ilerde birgun cache sistemini değiştirmek istersek yapıcagımız tek sey bu

            //İnjection çalışması içinde alttakini yazmamız gerkeiyor
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
