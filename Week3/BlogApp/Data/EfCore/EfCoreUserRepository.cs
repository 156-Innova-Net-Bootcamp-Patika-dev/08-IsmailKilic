using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Data.EfCore
{
    public class EfCoreUserRepository : EfCoreRepository<User, BlogContext>
    {
        private BlogContext _blogContext;
        public EfCoreUserRepository(BlogContext context) : base(context)
        {
            _blogContext = context;
        }

        public User GetByEmail(string email)
        {
            var user = _blogContext.Users
                .Where(x => x.Email == email).FirstOrDefault();
            return user;
        }
    }
}
