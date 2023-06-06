using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases
{
    public class GetByPostIdCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
    }

    public class GetByPostIdCommandHandler : IRequestHandler<GetByUserIdCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetByPostIdCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetByUserIdCommand request, CancellationToken cancellationToken)
        {
            ICollection<Post> posts = dbContext.Posts.Include(p => p.Comments).ThenInclude(c => c.Replies).ToList();
            Post? post = posts.FirstOrDefault(c => c.Id == request.Id);
            GetPostDTO postDTO = mapper.Map<GetPostDTO>(post);
            postDTO.Comments = mapper.Map<ICollection<GetCommentDTO>>(post.Comments);
            return new OkObjectResult(postDTO);
        }
    }
}



