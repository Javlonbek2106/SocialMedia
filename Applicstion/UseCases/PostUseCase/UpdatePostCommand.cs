using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.UserUseCase
{
    public class UpdatePostCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid UserId { get; set; }
    }
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;

        public UpdatePostCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            Post? post = dbContext.Posts.FirstOrDefault(x => x.Id == request.Id);
            dbContext.Posts.Update(post);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(post);
        }
    }
}
