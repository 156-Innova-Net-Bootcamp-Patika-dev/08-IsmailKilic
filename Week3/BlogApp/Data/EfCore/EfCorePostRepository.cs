using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Data.EfCore
{
    public class EfCorePostRepository : EfCoreRepository<Post, BlogContext>
    {
        private readonly BlogContext blogContext;
        public EfCorePostRepository(BlogContext context) : base(context)
        {
            blogContext = context;
        }

        public async Task<List<Post>> GetPosts()
        {
            var posts = await blogContext.Posts.Include(x => x.Category)
                .Include(x => x.User).ToListAsync();
            return posts;
        }
    }
}
