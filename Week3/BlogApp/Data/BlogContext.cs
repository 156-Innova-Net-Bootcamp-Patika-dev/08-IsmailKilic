using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
