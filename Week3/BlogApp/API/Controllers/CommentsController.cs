﻿using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto model)
        {
            await _service.CreateComment(model);
            return Created("~api/comments", new { message = "Kayıt başarılı" });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var context = HttpContext;
            var user = (AuthUser)context.Items["User"];

            _service.DeleteById(id, user.Id);
            return NoContent();
        }
    }
}
