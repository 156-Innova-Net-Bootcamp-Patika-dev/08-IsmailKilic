using System;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        private const string cacheKey = "categoryKey";
        private readonly IMemoryCache _memCache;

        public CategoriesController(ICategoryService service, IMemoryCache memCache)
        {
            _service = service;
            _memCache = memCache;
        }

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>List<Category></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            if (_memCache.TryGetValue(cacheKey, out object list))
                return Ok(list);

            var categories = _service.GetAll();
            _memCache.Set(cacheKey, categories, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(3),
                Priority = CacheItemPriority.Normal
            });
            return Ok(categories);
        }

        /// <summary>
        /// Creates a new category
        /// User needs to authorize to use this route
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto model)
        {
            await _service.CreateCategory(model);
            return Created("~api/categories", new { message = "Kayıt başarılı" });
        }
    }
}
