using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    public class Comment : BaseAuditableEntity
    {
        public string Text { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public Guid? PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }
        public Guid? CommentId { get; set; }
        [ForeignKey(nameof(CommentId))]
        public Comment? comment { get; set; }
    
        public ICollection<Comment>? Replies { get; set; }
    }
}
