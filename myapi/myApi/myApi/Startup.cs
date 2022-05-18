using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using myApi.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace myApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "URL is static")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.Filters.Add(
                       new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                setupAction.Filters.Add(
                       new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                setupAction.Filters.Add(
                       new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                setupAction.Filters.Add(
                       new ProducesDefaultResponseTypeAttribute());
            });
            services.AddControllers();

            services.AddDbContext<CustomerContext>(opt =>
                opt.UseInMemoryDatabase("InMemoryCustomerDB")
             );

            services.AddDbContext<CategoryContext>(opt =>
               opt.UseInMemoryDatabase("InMemoryCategoryDB")
            );

            services.AddDbContext<ProductContext>(opt =>
               opt.UseInMemoryDatabase("InMemoryProductB")
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("OpenAPISpecificationCustomer", new OpenApiInfo { Title = "Customer", Version = "v1" });               
                c.SwaggerDoc("OpenAPISpecificationCategory", new OpenApiInfo { Title = "Category", Version = "v2" });
                c.SwaggerDoc("OpenAPISpecificationProduct", new OpenApiInfo { Title = "Product", Version = "v3" });
                //c.SwaggerDoc("OpenAPISpecificationWeatherDefault", new OpenApiInfo { Title = "Weather", Version = "v3" });


                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                c.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error/index.html");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
                    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/OpenAPISpecificationCustomer/swagger.json", "API Customer");
                c.SwaggerEndpoint("/swagger/OpenAPISpecificationCategory/swagger.json", "API Category");
                c.SwaggerEndpoint("/swagger/OpenAPISpecificationProduct/swagger.json", "API Product");
                //c.SwaggerEndpoint("/swagger/OpenAPISpecificationWeatherDefault/swagger.json", "API Weather");

                // custom CSS
                c.InjectStylesheet("/swagger-ui/custom.css");
            });

            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors();

        }
        
    }
}
