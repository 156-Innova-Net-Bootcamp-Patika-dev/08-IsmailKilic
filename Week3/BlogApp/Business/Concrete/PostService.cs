using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Data.EfCore;
using Entities.Concrete;
using Entities.Dtos;
using AutoMapper;
using Slugify;
using System;
using Business.Exceptions;

namespace Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly EfCorePostRepository _postCepository;
        private readonly EfCoreUserRepository _userRepository;
        private readonly EfCoreCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PostService(EfCorePostRepository repository,
            EfCoreUserRepository userRepository,
             EfCoreCategoryRepository categoryRepository,
             IMapper mapper)
        {
            _postCepository = repository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreatePost(CreatePostDto model)
        {
            var userExisted = _userRepository.Get(x => x.Id == model.UserId);
            if (userExisted == null)
            {
                throw new BadRequestException("User not found");
            }

            var categoryExisted = _categoryRepository.Get(x => x.Id == model.CategoryId);
            if (categoryExisted == null)
            {
                throw new BadRequestException("Category not found");
            }

            SlugHelper helper = new SlugHelper();
            var post = _mapper.Map<Post>(model);
            post.Slug = helper.GenerateSlug(model.Title + "-" + DateTime.Now.ToLongTimeString());
            post.CreatedAt = DateTime.Now;
            post.Category = categoryExisted;
            post.User = userExisted;
            await _postCepository.Add(post);
        }

        public Post GetOneBySlug(string slug)
        {
            var post = _postCepository.Get(x => x.Slug == slug,
                (x => x.Category), (x => x.User), (x => x.Comments));
            return post;
        }

        public List<Post> GetBySlug(string slug)
        {
            var posts = _postCepository.GetList(x => x.Category.Slug == slug, (x => x.Category), (x => x.User));
            return posts;
        }

        public async Task<List<Post>> GetAll()
        {
            var posts = await _postCepository.GetPosts();
            return posts;
        }
    }
}
