using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases
{
    public class GetAllPostCommand : IRequest<IActionResult>
    {
    }

    public class GetAllPostCommandHandler : IRequestHandler<GetAllPostCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllPostCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAllPostCommand request, CancellationToken cancellationToken)
        {
            ICollection<Post> posts = dbContext.Posts.Include(p => p.Comments).ThenInclude(c => c.Replies).ToList();
            ICollection<GetPostDTO> postDTOs = new List<GetPostDTO>();
            foreach (var post in posts)
            {
                GetPostDTO postDTO = mapper.Map<GetPostDTO>(post);
                postDTO.Comments = mapper.Map<ICollection<GetCommentDTO>>(post.Comments);
                postDTOs.Add(postDTO);
            }
            return new OkObjectResult(postDTOs);
        }

    }
}
