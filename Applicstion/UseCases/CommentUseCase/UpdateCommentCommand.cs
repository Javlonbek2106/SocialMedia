using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases
{
    public class UpdateCommentCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
        public string CommentName { get; set; }
        public byte Age { get; set; }
    }
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, IActionResult>
    {
        private IApplicationDbContext dbContext;

        public UpdateCommentCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? comment = dbContext.Comments.FirstOrDefault(x => x.Id == request.Id);
            dbContext.Comments.Update(comment);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(comment);
        }
    }
}
