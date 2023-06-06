using Application.UseCases;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SocialMediaWebApi.Filters;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand userCommand)
        {
            return await _mediator.Send(userCommand);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByUserId([FromBody] GetByUserIdCommand userIdCommand)
        {
            return await _mediator.Send(userIdCommand);
        }

        [HttpGet]
        [Route("[action]")]
        [LazyCache(5,10)]
       // [OutputCache(Duration = 20)]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUserCommand());
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand userCommand)
        {
            return await _mediator.Send(userCommand);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand userCommand)
        {
            return await _mediator.Send(userCommand);
        }
    }
}
