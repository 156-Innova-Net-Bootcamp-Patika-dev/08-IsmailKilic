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

        [HttpGet]
        public async Task<IActionResult> GetAll(string slug)
        {
            List<Post> posts;
            if(slug == null) posts = await _service.GetAll();
            else posts = _service.GetBySlug(slug);

            return Ok(posts);
        }

        [HttpGet("{slug}")]
        public IActionResult GetBySlug(string slug)
        {
            var post = _service.GetOneBySlug(slug);

            return Ok(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto model)
        {
            await _service.CreatePost(model);
            return Created("~api/posts", new { message = "Kayıt başarılı" });
        }
    }
}
