using Microsoft.AspNetCore.Mvc;
using OspriTest.Features;
using OspriTest.Models;

namespace OspriTest.Interfaces
{
    /// <summary>
    /// An Interface for clients to implenet to talk to userController. 
    /// </summary>
    public interface IUsers
    {
        // GET api/Users/{id}
        ActionResult<User> Get(int id);

        // POST api/Users
        ActionResult<User> Post([FromBody] PutUserRequest value);
    }
}