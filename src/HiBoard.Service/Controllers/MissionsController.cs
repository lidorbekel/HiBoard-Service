using HiBoard.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiBoard.Service.Controllers;

[Route("api/missions")]
[ApiController]
[AllowAnonymous]
public class MissionsController : Controller
{
    private readonly MissionsService _service;

    public MissionsController(MissionsService service)
    {
        _service = service;
    }

    [HttpGet("{id}",Name = "GetMission")]
    public async Task<IActionResult> GetByIdAsync(int missionId)
    {
        var missionDto = await _service.GetByIdAsync(missionId);
        return Ok(missionDto);
    }
}