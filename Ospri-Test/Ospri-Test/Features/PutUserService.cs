using MediatR;
using OspriTest.Database;
using OspriTest.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace OspriTest.Features
{
    public class PutUserService : IRequestHandler<PutUserRequest, User>
    {
        public UsersDBContext DatabaseConnection;

        public PutUserService(UsersDBContext dBContext)
        {
            DatabaseConnection = dBContext;
        }


        public async Task<User> Handle(PutUserRequest request, CancellationToken cancellationToken)
        {
                var data = await DatabaseConnection.Users.AddAsync(new User()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    DateOfBith = request.DateOfBith
                }, cancellationToken).ConfigureAwait(false);
                DatabaseConnection.SaveChanges();
                return data.Entity;
        }
    }

    public record PutUserRequest : IRequest<User>
    {

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, MinimumLength = 20)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBith { get; set; }
    }
}

