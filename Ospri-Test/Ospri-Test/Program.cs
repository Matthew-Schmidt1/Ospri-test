using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OspriTest.Database;
using OspriTest.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OspriTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            CreateUserDb(host);
            host.Run();
        }

        private static void CreateUserDb(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<UsersDBContext>();
            DataGenerator.Initialize(services);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseSerilog((ctx, lc) =>
                    lc.ReadFrom.Configuration(ctx.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(new RenderedCompactJsonFormatter())
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();
                });
    }
}
