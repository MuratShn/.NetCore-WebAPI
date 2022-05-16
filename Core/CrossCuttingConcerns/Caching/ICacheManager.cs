using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        //key value seklinde tutuyoruz
        //key=> cache veridiğimiz isimdir 
        //Value => gelicek data herşey olabileceği için object şeklinde alıyoruz
        //duration ise cache ne kadar tutagıdır dk saat cinsenden nasıl istersek tutabılıriz
        void Add(string key, object value,int duration);

        //Cache'den bir data getirmek fakat bu data liste olabilir yada tek bir data döndürebilir ondan dolayı T(generic) olarak tutuyoruz
        //tabi key'in adınıda alıyoruz
        T Get<T>(string key);

        object Get(string key);


        ///Cache eklerken bakmamız gereken bir şeyde şuan cache'de varmı yokmu tabi onun içinde key almamız gerekiyor
        bool IsAdd(string key);

        //Birde Cache'den silme kısmı var tabi
        void Remove(string key);

        //isminde cat olanları ucur ,isminde cat olanlı ucur ayrıntı classında
        void RemovePattern(string pattern);
    }
}
