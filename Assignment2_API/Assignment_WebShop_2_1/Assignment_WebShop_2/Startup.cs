using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WebShop_2.Models;
using Assignment_WebShop_2.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Assignment_WebShop_2
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
            // Konfigur�ci� olvas�sa
            DbType dbType = Configuration.GetValue<DbType>("DbType");

            // Adatb�zis kontextus f�gg�s�gi befecskendez�se
            
            services.AddDbContext<WebShopContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            // A teend�k kezel�s�re szolg�l� service regisztr�l�sa az IoC t�rol�ba
            services.AddTransient<WebShopServices>();

            services.AddTransient<AccountService>();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(25); // max. 15 percig �l a munkamenet
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Categories}/{action=Index}/{id?}");
            });

            // Adatb�zis inicializ�l�sa
            DbInitializer.Initialize(serviceProvider, Configuration.GetValue<string>("ImageStore"));
        }
    }
}
