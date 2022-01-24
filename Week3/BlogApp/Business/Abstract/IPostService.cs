using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IPostService
    {
        Task CreatePost(CreatePostDto model);
        List<Post> GetBySlug(string slug);
        Post GetOneBySlug(string slug);
        Task<List<Post>> GetAll();
    }
}
