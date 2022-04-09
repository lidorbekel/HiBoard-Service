using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        [SwaggerOperation("Get Users List")]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _service.GetUsersAsync(cancellationToken);

            return Ok(users);
        }

        [SwaggerOperation("Get User")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _service.GetUserAsync(userId, cancellationToken);

            return Ok(user);
        }

        [SwaggerOperation("Create User")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _service.CreateUserAsync(userDto, cancellationToken);

            return Ok(user);
        }

        [SwaggerOperation("Update User")]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _service.UpdateUserAsync(userId, userDto, cancellationToken);

            return Ok(user);
        }

        [SwaggerOperation("Delete User")]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, CancellationToken cancellationToken)
        {
            await _service.DeleteUserAsync(userId, cancellationToken);

            return NoContent();
        }
    }
}