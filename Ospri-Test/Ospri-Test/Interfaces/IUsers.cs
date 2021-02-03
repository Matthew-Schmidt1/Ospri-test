using Microsoft.AspNetCore.Mvc;
using Ospri_Test.Models;

namespace Ospri_Test.Interfaces
{
    public interface IUsers
    {
        // GET api/Users/{id}
        ActionResult<User> Get(int id);
        
        // POST api/Users
        ActionResult<User> Post([FromBody] User value);
    }
}