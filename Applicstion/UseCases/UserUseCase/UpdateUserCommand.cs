using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases
{
    public class UpdateUserCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public byte Age { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IActionResult>
    {
        private IApplicationDbContext dbContext;

        public UpdateUserCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = dbContext.Users.FirstOrDefault(x => x.Id == request.Id);
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(user);
        }
    }
}
