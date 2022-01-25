using Entities.Concrete;

namespace Data.EfCore
{
    public class EfCoreCommentRepository : EfCoreRepository<Comment, BlogContext>
    {
        public EfCoreCommentRepository(BlogContext context) : base(context)
        {
        }
    }
}
