using AutoMapper;
using FluentValidation.AspNetCore;
using Kaspersky.BL.Services;
using Kaspersky.BL.Services.Impl;
using Kaspersky.Data.Common;
using Kaspersky.Data.Repository;
using Kaspersky.Data.Repository.Impl;
using Kaspersky.Web.Models.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Kaspersky.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KasperskyContext>(builder =>
            {
                builder.UseInMemoryDatabase(nameof(KasperskyContext));
                builder.EnableSensitiveDataLogging();
            });
            services.AddMvc().AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<BookValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Kaspersky API", Version = "v1" });
            });

            services.AddAutoMapper();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IIsbnValidator, Isbn13Validator>();
            services.AddSingleton<IIsbnValidator, Isbn10Validator>();
            services.AddSingleton<IIsbnService, IsbnService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, KasperskyContext context)
        {
            DbInitializer.Initialize(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kaspersky API V1");
            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "book",
                    template: "{controller=Book}/{action=Get}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
