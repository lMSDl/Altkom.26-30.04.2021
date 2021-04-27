using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            if (env.IsEnvironment("ExceptionDemo"))
            {
                app.Run(context => throw new Exception("Demo Exception"));
            }

            app.Use(EndpointCheck());

            app.UseRouting();

            app.Use(EndpointCheck());

            app.UseAuthorization();

            app.Map("/noEndpoint", appBuilder => app.Run(async context => await context.Response.WriteAsync("No Endpoint")));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/endpoint", async context => await context.Response.WriteAsync("Get Endpoint"));

                endpoints.MapControllers();
            });
        }

        private static Func<HttpContext, Func<Task>, Task> EndpointCheck()
        {
            return async (context, next) =>
            {
                Console.WriteLine(context.GetEndpoint()?.DisplayName ?? "unknown");
                await next();
            };
        }
    }
}
