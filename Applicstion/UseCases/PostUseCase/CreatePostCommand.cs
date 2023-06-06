using Application.Abstractions;
using Application.DTOs;
using Application.Events;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UseCases.UserUseCase
{
    public class CreatePostCommand : IRequest<IActionResult>
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(IApplicationDbContext dbContext, IMediator mediator, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Handle(CreatePostCommand postCommand, CancellationToken cancellationToken)
        {
            Post post = new Post
            {
                Text = postCommand.Text,
                UserId = postCommand.UserId,
               
            };
            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();
            User? user = dbContext.Users.FirstOrDefault(x => x.Id == post.UserId);
            //await mediator.Publish(new UserPostCreatedConsoleEvent
            //{
            //    UserName = user.UserName,
            //    Text = postCommand.Text
            //});
            await mediator.Publish(new OnUserPost
            {
                UserName = user.UserName,
                Text = postCommand.Text
            });
            return new OkObjectResult(_mapper.Map<GetPostDTO>(post));
        }
    }
}
