using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ospri_Test.Database
{
    public class DataGenerator
    {
        private static readonly ILogger _log = Log.Logger;



        public static void Initialize(IServiceProvider serviceProvider)
        {
            var log = _log.ForContext<DataGenerator>();
            log.Information("Initialize DataBases");

            var options = (DbContextOptions<UsersDBContext>)serviceProvider.GetService(typeof(DbContextOptions<UsersDBContext>));
            if (options == null)
            {
                log.Error("(DbContextOptions<UsersDBContext>)serviceProvider.GetService(typeof(DbContextOptions<UsersDBContext>)) returned null");
                throw new InvalidOperationException();
            }
            using (var context = new UsersDBContext(options))
            {
                if (context.Users.Any())
                {
                    log.Information("Database Already Exist!");
                    // Data was already seeded
                    return;
                }

                context.Add(new Models.User() { Address = "", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.SaveChanges();
            }

        }
    }
}
