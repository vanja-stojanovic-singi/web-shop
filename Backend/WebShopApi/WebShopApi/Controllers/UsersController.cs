using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopApi.Models;
using WebShopApi.Services;

namespace WebShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        { 
            this._usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                var users = await _usersService.GetUsers();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetById([FromRoute] string userId)
        {
            try
            {
                var user = await _usersService.GetUserById(new Guid(userId));

                if (user == null)
                {
                    return NotFound("User does not exist!");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User must be provided!");
            }

            try
            {
                var newUser = await _usersService.AddUser(user);

                return Ok(newUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<IEnumerable<User>>> Update([FromRoute] Guid userId, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User must be provided!");
            }

            try
            {
                var existingUser = await _usersService.GetUserById(userId);

                if (existingUser == null)
                {
                    return NotFound("User does not exist!");
                }

                var newUser = await _usersService.UpdateUser(userId, user);

                return Ok(newUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpPut("{userId}/change/password")]
        public async Task<ActionResult> ChangePassword([FromRoute] Guid userId, [FromBody] ChangePassword changePassword)
        {
            try
            {
                var existingUser = await _usersService.GetUserById(userId);

                if (existingUser == null)
                {
                    return NotFound("User does not exist!");
                }

                var result = await _usersService.ChangePassword(userId, changePassword.OldPassword, changePassword.NewPassword);

                if (result)
                {
                    return Ok();
                }

                return BadRequest("Invalid password!");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return Problem("Internal server error!");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<IEnumerable<User>>> Delete([FromRoute] Guid userId)
        {
            try
            {
                var user = await _usersService.GetUserById(userId);

                if (user != null)
                {
                    await _usersService.DeleteUser(userId);
                }

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return Problem("Internal server error!");
            }
        }
    }
}
