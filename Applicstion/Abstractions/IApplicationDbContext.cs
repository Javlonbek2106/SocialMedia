using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Post> Posts { get; }
        public DbSet<Comment> Comments { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
