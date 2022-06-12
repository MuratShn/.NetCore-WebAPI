using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class ExceptionMiddleware //apide hata oldugunda ne yapayım'ın kodu burda
    {
        private RequestDelegate _next; 

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try //uygulamadakı butun kodalı exception içine ealıyor
            {
                await _next(httpContext); //hata olmazsa devam
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e); //hada olursa handle(üstesinden gelmek) edıcek
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors; //birden fazla hata geliyor bazen

            if (e.GetType() == typeof(ValidationException)) //aldıgımız hata validation Exception(yazdıgımız kurallar) ise mesajı dondur
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors; //hatayı veya hataları errors'a atıyoruz
                httpContext.Response.StatusCode = 400;
                
                return httpContext.Response.WriteAsync(new ValidationsErrorDetail
                {
                    StatusCode = 400 ,//bad request donuyoruz cunku client kurallara uymamıs 
                    Message=e.Message,
                    Errors = errors
                }.ToString());
                
            }

            return httpContext.Response.WriteAsync(new ErrorDetails //aldıgımız hata validation hatası degılse hatayı don 
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
