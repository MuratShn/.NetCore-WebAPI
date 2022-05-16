using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache; //microsoft'un kendi kütüphanesinden alıyoruz bunu
        //bunuda coremoduleden injectini yapıyoruz

        public MemoryCacheManager()
        {
            //bizim memorcache injection'dan almamız gerkiyor
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value,TimeSpan.FromMinutes(duration)); //duration 10 ise 10 dk cache'de kalıcak
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key); //unboxing yapmak gerkiyor
        }

        public bool IsAdd(string key)
        {
            //trygetvalue sana istersen değeride(value) döndürüyo ama egerki değeri döndürmesini istemeyip sadece bool olarak döndürsün diyorsan
            //out _ yazman gerekioyr

            return _memoryCache.TryGetValue(key,out _); 
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemovePattern(string pattern)
        {
            //Senaryo => addproduct üstüne [IProductDal.Get] yazılmış. IProductService.get yazanları sil 
            //çalışma anında Bellekten silmeye yarıyo 
            //Reflection ile çalışma anında elinizde olan nesnelere hatta olmayanlara müdahele etme 


            //Cache dataları EntriesCollection diye birşeyin içinde tutuluyor
            //altta dediğimiz bellekte tipi MemoryCache olan EntriesCollection'nı bul
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            


            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>(); //herbir cache elemanını gez

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
            //Kısaca veriğimiz cache key'ini alıyoruz parametre olarak sonra bütün cacheler içinde dolaşıp aynı olanları siliyoruz
        }
    }
}
