using Domain.Common;

namespace Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string UserName { get; set; }
        public byte Age { get; set; }   
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
