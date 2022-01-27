using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    /// <summary>
    /// Interface to manage category operations
    /// </summary>
    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryDto model);
        List<Category> GetAll();
    }
}
