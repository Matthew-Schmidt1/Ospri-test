using MediatR;
using Ospri_Test.Database;
using Ospri_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ospri_Test.Features
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
            var data = await DatabaseConnection.Users.AddAsync(request.Data).ConfigureAwait(false);
            await DatabaseConnection.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return data.Entity;
        }
    }

    public record PutUserRequest : IRequest<User>
    {
        public User Data { get; set; }
    }
}

