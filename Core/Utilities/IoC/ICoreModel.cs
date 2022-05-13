using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection); //servisleri bu metot yuklicek 

        //Bussinnes'daki AutoFact'en farkı => bus. katmanınadaki bus.seviyesi yani nortwind projesi seviyesi
        //ama core(bunu) katmanındakini genel bağımlılıkları ayarlamak için yapıyor olucaz
    }
}