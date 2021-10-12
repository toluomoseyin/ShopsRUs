using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopRUs.Application.Services;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Data;
using ShopRUs.Infrastructure.Repositories.EFCoreRepositories;
using ShopRUs.Infrastructure.Seeder;
using ShopRUs.Infrastructure.Services;
using System;

namespace ShopRUs.API
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
            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddControllers();
            services.AddSingleton(new DatabaseConfig { Name = Configuration["DatabaseName"] });
            services.AddSingleton<IShopRUsDapperSeeder, ShopRUsDapperSeeder>();
            services.AddDbContext<ShopRUsDbContext>(
            options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "ShopRUs.API",
                    Description = "ShopsRUs is a existing retail outlet that provide discount to their customers on all their web/mobile platforms. This is an API built to provide capabilities to calculate discounts, generate the total costs and generate the invoices for customers",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Tolu Omoseyin",
                        Email = "toluomoseyin01@gmail.com",
                        Url = new Uri("https://tolu-portfolio.netlify.app/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                    }) ;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopRUs.API v1"));
            }

            ShopRUsSeeder.SeedDatabase(app).Wait();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //serviceProvider.GetService<IShopRUsDapperSeeder>().Setup();
        }
    }
}
