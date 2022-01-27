using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Abstract
{
    /// <summary>
    /// Interface to manage comment operations
    /// </summary>
    public interface ICommentService
    {
        Task CreateComment(CreateCommentDto model);
        void DeleteById(int id, int userId);
    }
}
