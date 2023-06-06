using Application.UseCases;
using Application.UseCases.UserUseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand postCommand)
        {
            return await _mediator.Send(postCommand);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByPostId([FromBody] GetByPostIdCommand postIdCommand)
        {
            return await _mediator.Send(postIdCommand);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllPosts()
        {
            return await _mediator.Send(new GetAllPostCommand());
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand postCommand)
        {
            return await _mediator.Send(postCommand);

        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeletePost([FromBody] DeletePostCommand postCommand)
        {
            return await _mediator.Send(postCommand);
        }
    }
}
