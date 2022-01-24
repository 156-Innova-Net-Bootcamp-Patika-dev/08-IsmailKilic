using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task CreateCategory(CreateCategoryDto model);
        List<Category> GetAll();
    }
}
