using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;
        public PostsController(IPostService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all posts
        /// If slug provided, returns posts by category
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string slug)
        {
            List<Post> posts;
            if(slug == null) posts = await _service.GetAll();
            else posts = _service.GetBySlug(slug);

            return Ok(posts);
        }

        /// <summary>
        /// Returns one post by slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var post = _service.GetOneBySlug(slug);

            return Ok(post);
        }

        /// <summary>
        /// Creates a new post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto model)
        {
            await _service.CreatePost(model);
            return Created("~api/posts", new { message = "Kayıt başarılı" });
        }
    }
}
