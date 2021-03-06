using Business.Abstract;
using Business.Concrete;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Default

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
            #endregion


            //Birisi senden IProductService isterse Ona bi tane ProductManager olu?tur ver
            //e?er bir ba??ml?l?k g?r?rsen bu tipte onun kar??l??? budur

            //services.AddSingleton<IProductService,ProductManager>(); // ProductManager da IProduct Dala bag?ml? ondan hata ver?cek alttak? g?b? devam etmem?z?m gerek?yor
            //services.AddSingleton<IProductDal, EfProductDal>();



            // Kaynak Paylas?m? ac?yoruz d?sardan b?r? ?stek atab?lmes? ?c?n yap?l?yor configure k?sm?dna devam ed?yor
            services.AddCors(); 
            //


            //Authentication ekleme k?sm?

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecuritykey(tokenOptions.SecurityKey)
                    };
                });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Bir tane core module ekled?k yarin oburgun baska core modullerde(injectionlarda) ekleyebilirim
            //ve alta istegi?im kadar module ekleyeb?ley?m istiyorum
            //Biz yaz?caz Extension yaz?caz

            services.AddDependencyResolvers(new ICoreModule[]
          {
                new CoreModule(),
          });
            //
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }
            app.ConfigureCustomExceptionMiddleware();

            app.UseCors(b=>b.WithOrigins("http://localhost:54066", "http://localhost:4200").AllowAnyHeader()); 
            //buda nererele izin verd?g?m?z? yaz?yor
            //allowany header ise get,put,post... hepsini izin ver

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
