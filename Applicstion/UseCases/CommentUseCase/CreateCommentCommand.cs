using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.CommentUseCase
{
    public class CreateCommentCommand : IRequest<IActionResult>
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? CommentId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateCommentCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(CreateCommentCommand employeeCommand, CancellationToken cancellationToken)
        {
            Comment comment = new Comment()
            {
                Text = employeeCommand.Text,
                UserId = employeeCommand.UserId,
                PostId = employeeCommand.PostId,
                CommentId = employeeCommand.CommentId
            };
            await dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(comment);
        }
    }
}