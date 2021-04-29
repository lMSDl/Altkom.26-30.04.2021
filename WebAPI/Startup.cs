using DataService;
using DataService.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Models.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPI.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.ResponseCompression;
using WebAPI.Filters;
using WebAPI.Hubs;

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
            services.AddControllers()
                .AddFluentValidation(/*options => 
                    options.RegisterValidatorsFromAssemblyContaining<EducatorValidator>()*/);
            services.AddTransient<IValidator<Educator>, EducatorValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            services.AddSingleton<IService<Student>, StudentsService>();
            services.AddSingleton<IService<Educator>, Service<Educator>>();
            services.AddSingleton<IUsersService, UsersService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(AuthService.Key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IAuthService, AuthService>();

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.AddScoped<SampleActionFilter>();
            services.AddSingleton<SampleAsyncActionFilter>();

            services.AddSignalR();
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

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<StudentsHub>("signalR/Students");

                endpoints.MapGet("/endpoint", async context => await context.Response.WriteAsync("Get Endpoint"));

                endpoints.MapControllers();
            });

            app.Map("/noEndpoint", appBuilder => app.Run(async context => await context.Response.WriteAsync("No Endpoint")));
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
