using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/missions")]
    public class MissionsController : Controller
    {
        private readonly MissionsService _service;

        public MissionsController(MissionsService service)
        {
            _service = service;
        }

        [SwaggerOperation("Get Missions List")]
        [HttpGet]
        public async Task<IActionResult> GetMissionsAsync(CancellationToken cancellationToken)
        {
            var missions = await _service.GetMissionsAsync(cancellationToken);

            return Ok(missions);
        }

        [SwaggerOperation("Get Mission")]
        [HttpGet("{missionId}")]
        public async Task<IActionResult> GetMissionAsync(int missionId, CancellationToken cancellationToken)
        {
            var mission = await _service.GetMissionAsync(missionId, cancellationToken);

            return Ok(mission);
        }

        [SwaggerOperation("Create Mission")]
        [HttpPost]
        public async Task<IActionResult> CreateMission([FromBody] MissionDto missionDto,
            CancellationToken cancellationToken)
        {
            var mission = await _service.CreateMissionAsync(missionDto, cancellationToken);

            return Ok(mission);
        }

        [SwaggerOperation("Update Mission")]
        [HttpPatch("{missionId}")]
        public async Task<IActionResult> UpdateMission(int missionId, [FromBody] MissionDto missionDto, CancellationToken cancellationToken)
        {
            var mission = await _service.UpdateMissionAsync(missionId, missionDto, cancellationToken);

            return Ok(mission);
        }

        [SwaggerOperation("Delete Mission")]
        [HttpDelete("{missionId}")]
        public async Task<IActionResult> UpdateMission(int missionId, CancellationToken cancellationToken)
        {
            await _service.DeleteMissionAsync(missionId, cancellationToken);

            return NoContent();
        }
    }
}