using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ospri_Test.Features;
using Ospri_Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ospri_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase, IUsers
    {

        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<Models.User> Get(int id)
        {
            var result = _mediator.Send(new GetUserRequest() { Id = id }).GetAwaiter().GetResult();
            if (result != null)
                return result;
            return NoContent();
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<Models.User> Post([FromBody] PutUserRequest value)
        {
            var Id = _mediator.Send(value).GetAwaiter().GetResult();

            if (Id != null)
            {
                return Created(Request.QueryString.Value, Id);
            }
            else
            {
                return Ok("Post Failed but know error raised");
            }

        }

    }
}
