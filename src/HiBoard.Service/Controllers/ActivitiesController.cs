using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/inventory/activities")]
    public class ActivitiesController : Controller
    {
        private readonly ActivitiesService _service;

        public ActivitiesController(ActivitiesService service)
        {
            _service = service;
        }

        [SwaggerOperation("Get Activities List")]
        [HttpGet]
        public async Task<IActionResult> GetActivitiesAsync(CancellationToken cancellationToken)
        {
            var activities = await _service.GetActivitiesAsync(cancellationToken);

            return Ok(activities);
        }

        [SwaggerOperation("Get Activity")]
        [HttpGet("{activityId}")]
        public async Task<IActionResult> GetActivityAsync(int activityId, CancellationToken cancellationToken)
        {
            var activity = await _service.GetActivityAsync(activityId, cancellationToken);

            return Ok(activity);
        }

        [SwaggerOperation("Create Activity")]
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] ActivityDto activityDto,
            CancellationToken cancellationToken)
        {
            var activity = await _service.CreateActivityAsync(activityDto, cancellationToken);

            return Ok(activity);
        }

        [SwaggerOperation("Update Activity")]
        [HttpPatch("{activityId}")]
        public async Task<IActionResult> UpdateActivity(int activityId, [FromBody] ActivityDto activityDto, CancellationToken cancellationToken)
        {
            var activity = await _service.UpdateActivityAsync(activityId, activityDto, cancellationToken);

            return Ok(activity);
        }

        [SwaggerOperation("Delete Activity")]
        [HttpDelete("{activityId}")]
        public async Task<IActionResult> UpdateActivity(int activityId, CancellationToken cancellationToken)
        {
            await _service.DeleteActivityAsync(activityId, cancellationToken);

            return NoContent();
        }
    }
}