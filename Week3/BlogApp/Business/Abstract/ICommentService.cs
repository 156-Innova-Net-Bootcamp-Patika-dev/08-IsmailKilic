using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICommentService
    {
        Task CreateComment(CreateCommentDto model);
        void DeleteById(int id);
    }
}
