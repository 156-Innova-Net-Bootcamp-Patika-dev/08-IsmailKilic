using Entities.Concrete;

namespace Data.EfCore
{
    public class EfCoreCategoryRepository : EfCoreRepository<Category, BlogContext>
    {
        public EfCoreCategoryRepository(BlogContext context) : base(context)
        {
        }

    }
}
