using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Business.Abstract;
using Data.EfCore;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto model)
        {
            var response = _userService.Login(model);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            await _userService.Register(model);
            return Created("~api/auth/register", new { message = "Registration successful" });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Hello world");
        }
    }
}
