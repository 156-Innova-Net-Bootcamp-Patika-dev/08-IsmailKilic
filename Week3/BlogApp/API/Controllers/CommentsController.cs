using System.Threading.Tasks;
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

        /// <summary>
        /// Creates a new comment
        /// User needs to authorize to use this route
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto model)
        {
            await _service.CreateComment(model);
            return Created("~api/comments", new { message = "Kayıt başarılı" });
        }

        /// <summary>
        /// Deletes comment by id
        /// Users needs to authorize to use this route
        /// They can just delete their comments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
