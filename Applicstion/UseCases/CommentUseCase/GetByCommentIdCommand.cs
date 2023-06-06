using Application.Abstractions;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases
{
    public class GetByCommentIdCommand : IRequest<IActionResult>
    {
        public Guid Id { get; set; }
    }

    public class GetByCommentIdCommandHandler : IRequestHandler<GetByCommentIdCommand, IActionResult>
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public GetByCommentIdCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Handle(GetByCommentIdCommand request, CancellationToken cancellationToken)
        {
            List<Comment> comments = dbContext.Comments.Include(c => c.Replies).ToList();
            Comment? comment = comments.FirstOrDefault(c => c.Id == request.Id);
            GetCommentDTO commentDTO = mapper.Map<GetCommentDTO>(comment);
            commentDTO.Comments = mapper.Map<ICollection<GetCommentDTO>>(comment.Replies);
            return new OkObjectResult(commentDTO);
        }
    }
}

