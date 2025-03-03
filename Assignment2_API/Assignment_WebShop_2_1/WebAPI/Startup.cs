using System;
using System.IO;
using System.Reflection;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddDbContext<WebShopContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<WebShopContext>()
            .AddDefaultTokenProviders();


            services.AddTransient<WebShopServices>();

            //services.AddTransient<AccountService>();

            //services.AddHttpContextAccessor();

            services.AddControllers();

            // Swagger generator regisztr�l�sa
            services.AddSwaggerGen(c =>
            {
                // (n�v, OpenApiInfo) p�rok megad�sa sz�ks�ges
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });

                // XML API dokument�ci� felhaszn�l�sa a Swaggerben
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Swagger haszn�lata (JSON v�gpontok)
            app.UseSwagger();

            // Swagger UI enged�lyez�se (b�ng�szhet� HTML v�gpontok)
            app.UseSwaggerUI(c =>
            {
                // a JSON v�gpont megad�sa (�s enged�lyez�se sz�ks�ges)
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Adatb�zis inicializ�l�sa
            DbInitializer.Initialize(serviceProvider, Configuration.GetValue<string>("ImageStore"));
        }
    }
}
