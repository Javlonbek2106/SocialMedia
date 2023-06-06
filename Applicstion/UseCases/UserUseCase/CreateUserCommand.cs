using Application.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases
{
    public class CreateUserCommand : IRequest<IActionResult>
    {
        public string UserName { get; set; }
        public byte Age { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;

        public CreateUserCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            User user = new User
            {
                UserName = command.UserName,
                Age = command.Age
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return new OkObjectResult(user);
        }
    }
}
