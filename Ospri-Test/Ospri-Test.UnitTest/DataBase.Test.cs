using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OspriTest.Database;
using OspriTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Opri_Test.UnitTest
{
    public class DataBase
    {
        [Test]
        public void BasicAddingUserTest()
        {
            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;
            
            // Insert seed data into the database using one instance of the context
            using (var context = new UsersDBContext(options,null))
            {
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new UsersDBContext(options,null))
            {
                List<User> users = context.Users.Select(s => s).ToList();
                Assert.AreEqual(1, users.Count);
            }
        }

        [Test]
        public void BasicFindingCorrectByNameUserTest()
        {
            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new UsersDBContext(options, null))
            {
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Correct", LastName = "Test1234" });
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new UsersDBContext(options, null))
            {
                var user = context.Users.FirstOrDefault(s => s.FirstName.Equals("Correct", StringComparison.InvariantCultureIgnoreCase));
                Assert.IsNotNull(user);
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.FirstName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.LastName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.Address));
                Assert.IsTrue(user.DateOfBith > DateTime.Now.AddDays(-1));
                Assert.IsTrue(user.LastName.Equals("Test1234", StringComparison.InvariantCultureIgnoreCase));
            }
        }

        [Test]
        public void BasicFindingCorrectByIdUserTest()
        {
            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new UsersDBContext(options, null))
            {
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Correct", LastName = "Test1234" });
                context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "Seed", LastName = "Test" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new UsersDBContext(options, null))
            {
                var user = context.Users.FirstOrDefault(s => s.Id == 3);
                Assert.IsNotNull(user);
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.FirstName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.LastName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.Address));
                Assert.IsTrue(user.DateOfBith > DateTime.Now.AddDays(-1));
                Assert.IsTrue(user.LastName.Equals("Test1234", StringComparison.InvariantCultureIgnoreCase));
            }
        }

        [Test]
        public void TestDbValidationOfDataModel()
        {
            var options = new DbContextOptionsBuilder<UsersDBContext>()
                        .UseInMemoryDatabase(databaseName: "User")
                        .Options;
            try
            {
                // Insert seed data into the database using one instance of the context
                using (var context = new UsersDBContext(options, null))
                {
                    context.Add(new User() { Address = "12345", DateOfBith = DateTime.Now, FirstName = "", LastName = "Test" });
                    context.SaveChanges();
                }
            }
            catch (ValidationException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The FirstName field is required"));
            }
        }
    }
}