using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Middleware;

namespace Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Use2Middleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMiddleware(typeof(Use1Middleware));
            app.UseMiddleware<Use1Middleware>();

            app.Map("/query1", Query1);

            //app.UseMiddleware<Use2Middleware>();
            //Use2ApplicationBuilderExtensions.UseUse2Middleware(app);
            app.UseUse2Middleware();

            app.Map("/query2", appBuilder =>
            {
                appBuilder.Run(async context => {

                    Console.WriteLine("Begin Query2Run");
                    await context.Response.WriteAsync("Hello from Query2Run!");
                    Console.WriteLine("End Query2Run");
                });
            });

            app.UseMiddleware<RunMiddleware>();
        }

        private static void Query1(IApplicationBuilder appBuilder)
        {
            appBuilder.Use(async (context, next) =>
            {
                Console.WriteLine("Begin Query1Use");
                await next();
                Console.WriteLine("End Query1Use");
            });

            appBuilder.Run(async context =>
            {

                Console.WriteLine("Begin Query1Run");
                await context.Response.WriteAsync("Hello from Query1Run!");
                Console.WriteLine("End Query1Run");
            });
        }
    }
}
