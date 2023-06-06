using AutoMapper;
using Domain.Entities;
using Application.DTOs;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDTO>();
            CreateMap<Post, GetPostDTO>();
            CreateMap<Comment, GetCommentDTO>().ReverseMap();
        }
    }
}
