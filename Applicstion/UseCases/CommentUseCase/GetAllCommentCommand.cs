using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases
{
    public class GetAllCommentCommand : IRequest<IActionResult>
    {
    }

    public class GetAllCommentCommandHandler : IRequestHandler<GetAllCommentCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetAllCommentCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetAllCommentCommand request, CancellationToken cancellationToken)
        {
            List<Comment> comments = dbContext.Comments.Include(c => c.Replies).ToList();
            List<GetCommentDTO> commentDTOs = new List<GetCommentDTO>();
            foreach (var comment in comments)
            {
                GetCommentDTO commentDTO = mapper.Map<GetCommentDTO>(comment);
                commentDTO.Comments = mapper.Map<ICollection<GetCommentDTO>>(comment.Replies);
                commentDTOs.Add(commentDTO);
            }
            return new OkObjectResult(commentDTOs);
        }

    }
}
