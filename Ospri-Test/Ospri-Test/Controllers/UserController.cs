using MediatR;
using Microsoft.AspNetCore.Mvc;
using OspriTest.Features;
using OspriTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OspriTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUsers
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<Models.User> Get(int id)
        {
            var response = _mediator.Send(new GetUserRequest() { Id = id }).GetAwaiter().GetResult();
            if (response != null)
                return response;
            return NoContent();
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<Models.User> Post([FromBody] PutUserRequest value)
        {
            var response = _mediator.Send(value).GetAwaiter().GetResult();

            if (response != null)
            {
                return Created(Request.QueryString.Value, response);
            }
            else
            {
                return Ok("Post Failed but know error raised");
            }

        }

    }
}
