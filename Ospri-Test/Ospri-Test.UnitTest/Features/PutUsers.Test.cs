using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OspriTest.Database;
using OspriTest.Features;
using OspriTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OspriTest.UnitTest.Features
{
    class PutUsers
    {
        [Test]
        public async Task PutUserRequestAsyncTestFailure()
        {

            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new UsersDBContext(options, null))
            {
                var service = new PutUserService(context);
                var cancelToken = new CancellationToken();
                try
                {
                    var response = await service.Handle(new PutUserRequest { Address = "123", FirstName = "", LastName = " ", DateOfBith = DateTime.Now }, cancelToken).ConfigureAwait(false);
                }
                catch (ValidationException ex)
                {
                    Assert.IsTrue(ex.Message.Contains("The FirstName field is required"));
                }
            }

        }

        [Test]
        public async Task PutUserRequestAsyncTestSuccess()
        {
            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new UsersDBContext(options, null))
            {
                var service = new PutUserService(context);
                var cancelToken = new CancellationToken();

                var response = await service.Handle(new PutUserRequest { Address = "12345", FirstName = "ab", LastName = "cd", DateOfBith = DateTime.Now }, cancelToken).ConfigureAwait(false);
                
                Assert.IsNotNull(response);
            }

        }

    }
}
