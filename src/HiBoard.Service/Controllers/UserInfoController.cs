using HiBoard.Application.Services;
using HiBoard.Domain;
using HiBoard.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace HiBoard.Service.Controllers
{
    [ApiController]
    [Route("api/userinfo")]
    public class UserInfoController : ControllerBase
    {
        private readonly UserInfoService _service;

        public UserInfoController(UserInfoService service)
        {
            _service = service;
        }

        [SwaggerOperation("Get User info")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfoAsync(CancellationToken cancellationToken)
        {
            var result = await _service.GetUserInfo(cancellationToken);
            var response = new HiBoardResponse<UserDto?>(result);
            
            return Ok(response);
        }
    }
}