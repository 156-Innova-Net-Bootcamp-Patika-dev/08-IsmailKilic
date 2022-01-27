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

        /// <summary>
        /// Login route
        /// User sends credentials, methods return user infos and jwt token
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Login Response</returns>
        [HttpPost("login")]
        public IActionResult Login(LoginDto model)
        {
            var response = _userService.Login(model);
            return Ok(response);
        }

        /// <summary>
        /// Register route
        /// It takes registerDto model, creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            await _userService.Register(model);
            return Created("~api/auth/register", new { message = "Registration successful" });
        }
    }
}
