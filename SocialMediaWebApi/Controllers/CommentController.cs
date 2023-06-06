using Application.UseCases;
using Application.UseCases.CommentUseCase;
using Application.UseCases.UserUseCase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand commentCommand)
        {
            return await _mediator.Send(commentCommand);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetByCommentId([FromBody] GetByCommentIdCommand commentIdCommand)
        {
            return await _mediator.Send(commentIdCommand);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllComments()
        {
            return await _mediator.Send(new GetAllCommentCommand());
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand commentCommand)
        {
            return await _mediator.Send(commentCommand);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentCommand commentCommand)
        {
            return await _mediator.Send(commentCommand);
        }
    }
}
