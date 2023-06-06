using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases
{
    public class GetByUserIdCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
    }

    public class GetByUserIdCommandHandler : IRequestHandler<GetByUserIdCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetByUserIdCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetByUserIdCommand request, CancellationToken cancellationToken)
        {
            ICollection<User> users = dbContext.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(c => c.Replies)
                .ToList();
            User? user = users.FirstOrDefault(c => c.Id == request.Id);
            GetUserDTO userDTO = mapper.Map<GetUserDTO>(user);
            userDTO.Posts = mapper.Map<List<GetPostDTO>>(user.Posts);
            return new OkObjectResult(userDTO);
        }
    }
}



