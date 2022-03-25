using HiBoard.Service.Resources;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiDotNetCore.Serialization.Objects;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers;

[Produces("application/vnd.api+json")]
[Consumes("application/vnd.api+json")]
[DisableRoutingConvention]
[Route("api/v1/hi-board/users")]
public class UsersController : JsonApiController<UserResource>
{
    public UsersController(IJsonApiOptions options, ILoggerFactory loggerFactory,
        IGetAllService<UserResource, int>? getAll = null, IGetByIdService<UserResource, int>? getById = null,
        IGetSecondaryService<UserResource, int>? getSecondary = null,
        IGetRelationshipService<UserResource, int>? getRelationship = null,
        ICreateService<UserResource, int>? create = null,
        IAddToRelationshipService<UserResource, int>? addToRelationship = null,
        IUpdateService<UserResource, int>? update = null,
        ISetRelationshipService<UserResource, int>? setRelationship = null,
        IDeleteService<UserResource, int>? delete = null,
        IRemoveFromRelationshipService<UserResource, int>? removeFromRelationship = null) : base(options,
        loggerFactory, getAll, getById, getSecondary, getRelationship, create, addToRelationship, update,
        setRelationship, delete, removeFromRelationship) { }

    [ApiExplorerSettings(GroupName = Constants.ResourceNames.User)]
    [SwaggerOperation("Get Users")]
    [ProducesResponseType(typeof(List<UserResource>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public override async Task<IActionResult> GetAsync(CancellationToken cancellationToken) =>
        await base.GetAsync(cancellationToken);

    [ApiExplorerSettings(GroupName = Constants.ResourceNames.User)]
    [SwaggerOperation("Get User by id")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status500InternalServerError)]
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken) =>
        await base.GetAsync(id, cancellationToken);

    [ApiExplorerSettings(GroupName = Constants.ResourceNames.User)]
    [SwaggerOperation("Create User")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public override async Task<IActionResult> PostAsync(UserResource activity,
        CancellationToken cancellationToken) =>
        await base.PostAsync(activity, cancellationToken);

    [ApiExplorerSettings(GroupName = Constants.ResourceNames.User)]
    [SwaggerOperation("Update User")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status500InternalServerError)]
    [HttpPatch("{id}")]
    public override async Task<IActionResult> PatchAsync(int id, UserResource activity,
        CancellationToken cancellationToken) =>
        await base.PatchAsync(id, activity, cancellationToken);

    [ApiExplorerSettings(GroupName = Constants.ResourceNames.User)]
    [SwaggerOperation("Delete User")]
    [ProducesResponseType(typeof(UserResource), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDocument), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public override async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken) => await base.DeleteAsync(id, cancellationToken);
}
