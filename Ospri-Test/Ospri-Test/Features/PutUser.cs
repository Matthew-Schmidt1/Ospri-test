using MediatR;
using OspriTest.Database;
using OspriTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OspriTest.Features
{
    public class PutUser : IRequestHandler<PutUserRequest, User>
    {
        public UsersDBContext DatabaseConnection;

        public PutUser(UsersDBContext dBContext)
        {
            DatabaseConnection = dBContext;
        }


        public async Task<User> Handle(PutUserRequest request, CancellationToken cancellationToken)
        {
            //If this was a real database i would assume some lag and want to run the method as a async request.
            var data = await DatabaseConnection.Users.AddAsync(new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                DateOfBith = request.DateOfBith
            }).ConfigureAwait(false);
            await DatabaseConnection.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return data.Entity;
        }
    }

    public record PutUserRequest : IRequest<User>
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBith { get; set; }
    }
}

