using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases
{
    public class DeleteUserCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteUserCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteEmployeeCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(DeleteUserCommand userCommand, CancellationToken cancellationToken)
        {
            User? user = await dbContext.Users.FindAsync(userCommand.Id);
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(true);
        }
    }
}
