using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiBoard.Service.Controllers;


    [Route("api/users")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;

        public UsersController(UsersService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            UserDto user = await _service.GetUserInfoAsync(userId);
            return Ok(user);
        }
    }
