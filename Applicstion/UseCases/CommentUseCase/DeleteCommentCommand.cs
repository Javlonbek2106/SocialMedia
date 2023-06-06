using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.CommentUseCase
{
    public class DeleteCommentCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteCommentCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(DeleteCommentCommand employeeCommand, CancellationToken cancellationToken)
        {
            Comment? comment = dbContext.Comments.FirstOrDefault(x => x.Id == employeeCommand.Id);
            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(comment);
        }
    }
}
