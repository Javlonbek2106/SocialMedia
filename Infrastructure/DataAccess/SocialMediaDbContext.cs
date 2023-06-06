using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Infrustructure.Interceptor;

namespace Infrastructure
{
    public class SocialMediaDbContext : DbContext, IApplicationDbContext
    {
        private readonly InterceptorClass _interceptor;
        public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options,
            InterceptorClass interceptor)
            : base(options)
        {
            _interceptor = interceptor;
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
