using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OspriTest.Configuration;
using OspriTest.Database;
using OspriTest.Features;
using OspriTest.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OspriTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SerilogConfig.SetupLogging(Configuration);
        }

        public IConfiguration Configuration { get; }
        private Settings _settings;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions()
                    .Configure<Settings>(Configuration.GetSection("Settings"))
                    .AddSingleton(Configuration);
            _settings = Configuration.GetSection(nameof(Settings)).Get<Settings>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OspriTest", Version = "v1" });
            });
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(GetUserRequest).Assembly);
                cfg.RegisterServicesFromAssemblies(typeof(PutUserRequest).Assembly);
            });
            services.AddDbContext<UsersDBContext>(options => {
                if (_settings.DatabaseSettings.UseInMemory)
                {
                    options.UseInMemoryDatabase(databaseName: "Users");
                }
                else
                {
                    options.UseSqlServer(_settings.DatabaseSettings.BuildConnectionString());
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OspriTest v1"));
            }

            app.UseHttpsRedirection();
            // Adding Logging
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            // Custom Logging middle ware
            app.AddLoggingMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
