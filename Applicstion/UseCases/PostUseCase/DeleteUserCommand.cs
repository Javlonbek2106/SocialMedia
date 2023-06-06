using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases
{
    public class DeletePostCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<DeletePostCommand, IActionResult>
    {
        private IApplicationDbContext dbContext;

        public UpdatePostCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            Post? post = dbContext.Posts.FirstOrDefault(x=>x.Id==request.Id);
            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(post);
        }
    }
}
