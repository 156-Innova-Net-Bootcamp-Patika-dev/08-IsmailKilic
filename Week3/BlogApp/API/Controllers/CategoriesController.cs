using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _service.GetAll();
            return Ok(categories);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            await _service.CreateCategory(model);
            return Created("~api/categories", new { message = "Kayıt başarılı" });
        }
    }
}
