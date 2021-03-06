﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.StaticFiles;

using MyRestAPI.Services;


using MyAspCoreRestAPI.MyMiddlewareExtensions;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MyRestAPI
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Register Service by using dependency injection
            services.AddSingleton<IMyProductsRepository, InMemMyProductsRepository>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            // Serve my app-specific default file, if present.
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                // A request to http://<app>/ will serve the default file (say index.html)  from wwwroot 
                //eg: http://localhost:61306

                // A request to http://<app>/StaticFiles/index.html will serve the index.html file from StaticFiles.
                //eg: http://localhost:61306/StaticFiles/index.html

                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });


            ConfigureAuth(app);
            app.UseMySample1Middleware();

            app.UseMvc();

            //app.UseSwaggerGen();
            app.UseSwagger();

            // Try the swagger UI endpoint .../swagger/ui/
            // Eg: http://localhost:61306/swagger/ui
            // Also check for .../swagger/v1/swagger.json
            app.UseSwaggerUi(); 
        }
    }
}
