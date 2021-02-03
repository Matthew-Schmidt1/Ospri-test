using MediatR;
using Microsoft.VisualBasic.CompilerServices;
using Ospri_Test.Database;
using Ospri_Test.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ospri_Test.Features
{
    public class GetUser : IRequestHandler<GetUserRequest, User>
    {
        public UsersDBContext DatabaseConnection;

        public GetUser(UsersDBContext dBContext)
        {
            DatabaseConnection = dBContext;
        }

        public Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            //If this was a real database i would assume some lag and want to run the method as a async request.

            return Task.Run(() =>
            {
                return DatabaseConnection.Users.FirstOrDefault(x => x.Id == request.Id);
            });
        }
    }

    public record GetUserRequest : IRequest<User>
    {
        public int Id { get; init; }
    }

}
