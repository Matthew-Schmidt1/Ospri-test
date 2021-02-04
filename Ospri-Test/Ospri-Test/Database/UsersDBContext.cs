using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OspriTest.Models;
using System.ComponentModel.DataAnnotations;

namespace OspriTest.Database
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext(DbContextOptions<UsersDBContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }


        /// <summary>
        /// From my Research to get it to valdiate the object we are sending 
        /// it we need to override this method and include validation
        /// https://stackoverflow.com/questions/39493779/does-ef-core-savechanges-validate-against-the-data-annotations/42532599
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }

            return base.SaveChanges();
        }

    }
}
