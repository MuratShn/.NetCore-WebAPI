using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Castle.DynamicProxy;

namespace Core.Aspects.AutoFac.Performance
{
    //Bunu sadece metotlarda kullanılırsanız o metodun ne kadar sürede calıstıgınız söyler ama bunu AspectInterceptorSelector'a yazarsak
    //bütün sistem hakkında bilgi alırız
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch; //timer

        public PerformanceAspect(int interval)
        {
            _interval = interval; //buda istenilen süre 30 saniye olsun işlem 30 saniye sürecek gibisinden düşünülebilir saniye cinnsinden
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start(); //metot başlamadan timer'i baslatıyoruz
        }

        protected override void OnAfter(IInvocation invocation)
        {
            //metot bitiminde geçen süreyi alıyorum(_stopwatch.Elapsed.TotalSeconds) eğer benim istediğim süreyi geçtiyse uyarı veriyorum ve timerimi sıfırlıyorum
            if (_stopwatch.Elapsed.TotalSeconds > _interval)  
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}"); 
            }
            _stopwatch.Reset();
        }
    }
}
