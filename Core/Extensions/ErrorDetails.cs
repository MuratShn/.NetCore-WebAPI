using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class ValidationsErrorDetail : ErrorDetails
    {
        //buda normalde yukarıdaydı bunu yapmamızın sebebi eğerki hata validasyon sebebinden dolayı oluştuysa
        //hataları errors ıcıne al (a ile basla 5den / kucuk ol gibi) ama egerkı hata baske bıseyden oturuyse 
        //o zaman bu null gıdıcek anlamsız olucak 
        public IEnumerable<ValidationFailure> Errors { get; set; } 

    }
}
