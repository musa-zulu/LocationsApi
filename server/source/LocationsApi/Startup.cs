using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LocationsApi.Infrastructure.Extension;
using LocationsApi.Service;
using Serilog;
using System.IO;
using LocationsApi.Service.Helpers;
using LocationsApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace LocationsApi
{
    public class Startup
    {
        private readonly IConfigurationRoot configRoot;
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;

            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            configRoot = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddController();

            services.AddDbContext(Configuration, configRoot);

            services.AddAutoMapper();

            services.AddAddScopedServices();

            services.AddSwaggerOpenAPI();
            
            services.AddMediatorCQRS();

            services.AddSingleton(Configuration);

            services.AddVersion();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureCustomExceptionMiddleware();

            app.ConfigureSwagger();

            log.AddSerilog();

            RunMigrations(service);

            ApiHelper.IntializeApiClient();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RunMigrations(IServiceProvider service)
        {
            // This returns the context.
            using var context = service.GetService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
