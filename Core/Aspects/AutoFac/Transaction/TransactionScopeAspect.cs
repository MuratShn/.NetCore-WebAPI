using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.AutoFac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception e)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }   
    }
}

//Transaction => Uygulamalarda tutarlılıgı korumak için kullanılan yöntem benim hesapta 100 tl var iso'ya 10 tl aktarıcam aynı sürecte 2 veri tabanı işi var 
//bendden para eksılıcek kereme para eklencek
//Benim hesabımdan para düştü fakat iso'ya ekleme yapılmadan sistem hata veri para havada kaldı
//bu durumda para bana iade edilmeli

/*
 bunun için ProductManager'da bi metot olustudugumuzu ve alttakı kodları ıcıne yazıdıgımızı varsayalım

using(TransactionScope ts = new TransactionScope ())
{
            try
            {
                add(data); => benden para çıktı 

                diğer işlemler => kod burda hata verebilir

                add(data) => İsmaile para eklendi
                
                ts.complete(); => eğer işlem başarısız tamamlandıysa uygula ve devam et
            }
            catch (Exception)
            {
                ts.dispose() => eğer yukarda hata aldıysak bütün işlemleri iptal et sanki hiç metot'da girilmemiş gibi bi değişiklik olmasın
            }
}

 */