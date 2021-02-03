using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ospri_Test.Database
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            var options = (DbContextOptions<UsersDBContext>)serviceProvider.GetService(typeof(DbContextOptions<UsersDBContext>));
            if (options == null)
            {
                throw new InvalidOperationException();
            }
            using (var context = new UsersDBContext(options))
            {
                if (context.Users.Any())
                {
                    // Data was already seeded
                    return;
                }

                context.Add(new Models.User() {Address="",DateOfBith= DateTime.Now,FirstName="Seed",LastName="Test" });
                context.SaveChanges();
            }
        }
    }
}
