using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.Api.Configuration;
using Products.Api.Extensions;
using Products.Api.Application.Services;
using Products.Api.Application.Validators;
using Products.Api.Data.Extensions;
using Products.Api.Data.Repository;
using Products.Api.Application.Filters;

namespace Products.Api
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
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidationFilter>();
                options.Filters.Add<ExceptionFilter>();
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());

            services.AddHealthChecks();
            services.AddSwagger();
            services.AddOptions()
                    .Configure<AppSettings>(Configuration);

            services.AddDbContext();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product Api V1");
            });

            app.UseMigrateDatabase();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to the Product API");
                });
            });
        }
    }
}
