using Domain.Entities;

namespace Application.DTOs
{
    public class GetCommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid PostId { get; set; }

        public ICollection<GetCommentDTO> Comments { get; set; }

    }
}
