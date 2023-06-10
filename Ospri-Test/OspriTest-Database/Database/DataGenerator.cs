using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OspriTest.Database
{
    public class DataGenerator
    {
        private static readonly Serilog.ILogger _log = Log.Logger;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var log = _log.ForContext<DataGenerator>();
            log.Information("Initialize DataBases");
            
            var options = (DbContextOptions<UsersDBContext>)serviceProvider.GetService(typeof(DbContextOptions<UsersDBContext>));
            var logFactory =(ILoggerFactory) serviceProvider.GetService(typeof(ILoggerFactory));
            
            if (options == null)
            {
                log.Error("(DbContextOptions<UsersDBContext>)serviceProvider.GetService(typeof(DbContextOptions<UsersDBContext>)) returned null");
                throw new InvalidOperationException();
            }
            using (var context = new UsersDBContext(options, logFactory))
            {
                if (context.Database.IsRelational())
                {
                    context.Database.Migrate();
                }
                if (context.Users.Any())
                {
                    log.Information("Database Already Exist!");
                    // Data was already seeded
                    return;
                }

                context.Add(new Models.User() { Address = "123456", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.SaveChanges();
            }

        }
    }
}
