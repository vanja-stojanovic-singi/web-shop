using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopApi.Auth.Services;
using WebShopApi.Models;
using WebShopApi.Services;
using WebShopApi.Services.Implementation;

namespace WebShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUsersService _usersService;

        public AuthController(JwtTokenService jwtTokenService, IUsersService usersService)
        {
            _jwtTokenService = jwtTokenService;
            _usersService = usersService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var isValid = await _usersService.Validate(model.Email, model.Password);

            if (isValid)
            {
                var loggedUser = await _usersService.GetLoggedUserByEmail(model.Email);
                var token = _jwtTokenService.GenerateToken(model.Email);
                loggedUser.Token = token;

                return Ok(loggedUser);
            }

            return Unauthorized("Invalid credentials");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            var existingUser = await _usersService.GetUserByEmail(newUser.Email);

            if (existingUser != null)
            {
                return BadRequest($"User with email address {newUser.Email} already exists!");
            }

            var createdUser = await _usersService.AddUser(newUser);
            var token = _jwtTokenService.GenerateToken(createdUser.Email);

            var loggedUser = new LoggedUser
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                Name = createdUser.Name,
                Token = token
            };

            return Ok(loggedUser);
        }
    }
}
