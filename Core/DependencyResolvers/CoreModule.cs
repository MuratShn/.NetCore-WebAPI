using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

            serviceCollection.AddSingleton<HttpContextAccessor, HttpContextAccessor>();
            //her istekte olusan context baslangıctan bitişe kadar request response arası

        }
    }
}
