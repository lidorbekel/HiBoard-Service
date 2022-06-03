using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/user")]
public class UsersController : ControllerBase
{
    private readonly UsersService _service;

    public UsersController(UsersService service)
    {
        _service = service;
    }

    [SwaggerOperation("Get User By JWT Token")]
    [HttpGet]
    public async Task<IActionResult> GetUserInfoAsync(CancellationToken cancellationToken)
    {
        var result = await _service.GetUserInfo(cancellationToken);
        var response = new HiBoardResponse<UserDto>(result);

        return Ok(response);
    }

    [SwaggerOperation("Get User (Manager) employees")]
    [HttpGet]
    [Route("{userId}/employees")]
    public async Task<IActionResult> GetUserEmployeesAsync(int userId, CancellationToken cancellationToken)
    {
        var users = await _service.GetUserEmployees(userId, cancellationToken);
        var response = new HiBoardResponse<IReadOnlyCollection<UserDto>>(users);
            
        return Ok(response);
    }
        
    [SwaggerOperation("Get User")]
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserAsync(int userId, CancellationToken cancellationToken)
    {
        var user = await _service.GetUserAsync(userId, cancellationToken);
        var response = new HiBoardResponse<UserDto>(user);
            
        return Ok(response);
    }

    [SwaggerOperation("Create User")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto,[FromQuery] int managerId, CancellationToken cancellationToken)
    {
        var user = await _service.CreateUserAsync(userDto,managerId, cancellationToken);
        var response = new HiBoardResponse<UserDto>(user);
            
        return Ok(response);
    }

    [SwaggerOperation("Update User")]
    [HttpPatch("{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto userDto, CancellationToken cancellationToken)
    {
        var user = await _service.UpdateUserAsync(userId, userDto, cancellationToken);
        var response = new HiBoardResponse<UserDto>(user);
            
        return Ok(response);
    }

    [SwaggerOperation("Delete User")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId, CancellationToken cancellationToken)
    {
        await _service.DeleteUserAsync(userId, cancellationToken);
            
        return NoContent();
    }
}