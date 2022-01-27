using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Data.EfCore;
using Entities.Concrete;
using Entities.Dtos;
using Slugify;
using System;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly EfCoreCategoryRepository _repository;
        private readonly IMapper _mapper;


        public CategoryService(EfCoreCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Uses slugHelper and creates a new category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateCategory(CreateCategoryDto model)
        {
            SlugHelper helper = new SlugHelper();
            var category = _mapper.Map<Category>(model);
            category.Slug = helper.GenerateSlug(model.Name + "-" + DateTime.Now.ToLongTimeString());
            await _repository.Add(category);
        }

        /// <summary>
        /// Returns all categories including their posts
        /// </summary>
        /// <returns>List<Category></returns>
        public List<Category> GetAll()
        {
            var categories = _repository.GetList(null, (x => x.Posts));
            return categories;
        }
    }
}
