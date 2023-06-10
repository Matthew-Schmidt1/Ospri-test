using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OspriTest.Database;
using OspriTest.Features;
using OspriTest.Models;
using System;
using System.Threading;

namespace OspriTest.UnitTest.Features
{
    public class GetUsers
    {
        [Test]
        public async System.Threading.Tasks.Task GetUserIDTestAsync()
        {
     
            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new UsersDBContext(options, null))
            {
                context.Add(new User() { Address = "142345678910111213141516171819a", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.Add(new User() { Address = "142345678910111213141516171819a", DateOfBith = DateTime.Now, FirstName = "Correct", LastName = "Test1234" });
                context.Add(new User() { Address = "142345678910111213141516171819a", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.SaveChanges();

                var Service = new GetUserService(context);
                var cancelToken = new CancellationToken();


                var response = await Service.Handle(new GetUserRequest() { Id = 1 }, cancelToken).ConfigureAwait(false);


                Assert.IsNotNull(response);
                Assert.IsTrue(response.Id == 1);
                Assert.IsTrue(response.FirstName == "Seed");
            }

        }
    }
}
