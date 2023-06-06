using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases
{
    public class GetAllUserCommand : IRequest<IActionResult>
    {
    }

    public class GetAllUserCommandHandler : IRequestHandler<GetAllUserCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAllUserCommand request, CancellationToken cancellationToken)
        {
            ICollection<User> users =  dbContext.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(c => c.Replies)
                .ToList();
            ICollection<GetUserDTO> userDTOs = new List<GetUserDTO>();
            foreach (var user in users)
            {
                GetUserDTO userDTO = mapper.Map<GetUserDTO>(user);
                userDTO.Posts = mapper.Map<List<GetPostDTO>>(user.Posts);
                userDTOs.Add(userDTO);
            }
            return new OkObjectResult(userDTOs);
        }

    }
}
