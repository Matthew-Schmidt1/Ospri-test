using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
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
    public class GetUserService : IRequestHandler<GetUserRequest, User>
    {
        public UsersDBContext DatabaseConnection;

        public GetUserService(UsersDBContext dBContext)
        {
            DatabaseConnection = dBContext;
        }

        public Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
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
