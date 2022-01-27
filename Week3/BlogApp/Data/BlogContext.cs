using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    /// <summary>
    /// Context implementation
    /// Takes connection string from api startup
    /// </summary>
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
