using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using СreditСalculator.AppData;
using СreditСalculator.Models;

namespace СreditСalculator
{
    public class Startup
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        /// <summary>
        /// Метод для добавления служб в контейнер
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Подключаем БД
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CreditContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
        }


        /// <summary>
        /// Метод для настройки конвейера HTTP- запросов
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Если приложение в процессе разработки
            if (env.IsDevelopment())
            {
                // то выводим информацию об ошибке, при наличии ошибки на страницу
                app.UseDeveloperExceptionPage();
            }
            else                            
            {   //выводим заглушку с ошибкой
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection(); //Работа с запросами с HTTP на HTTPS

            app.UseStaticFiles(); //Cтатические файлы. (css, js)

            app.UseRouting(); //Маршрутизация

            app.UseAuthorization(); //Авторизация

            // Устанавливаем  шаблон адреса, которые будут обрабатываться
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //  name: "TwoParametrRoute",
                //  pattern: "{controller}/{action}/{x}/{y}"
                //  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


            });
        }
    }
}
