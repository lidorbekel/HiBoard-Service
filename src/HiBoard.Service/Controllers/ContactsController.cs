using HiBoard.Service.Resources;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Controllers.Annotations;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HiBoard.Service.Controllers;

[DisableRoutingConvention]
[Route("api/v1/hi-board/accounts/{account-id}/contacts")]
public class ContactsController : JsonApiController<ContactResource>
{
    public ContactsController(IJsonApiOptions options, ILoggerFactory loggerFactory,
        IGetAllService<ContactResource, int>? getAll = null, IGetByIdService<ContactResource, int>? getById = null,
        IGetSecondaryService<ContactResource, int>? getSecondary = null,
        IGetRelationshipService<ContactResource, int>? getRelationship = null,
        ICreateService<ContactResource, int>? create = null,
        IAddToRelationshipService<ContactResource, int>? addToRelationship = null,
        IUpdateService<ContactResource, int>? update = null,
        ISetRelationshipService<ContactResource, int>? setRelationship = null,
        IDeleteService<ContactResource, int>? delete = null,
        IRemoveFromRelationshipService<ContactResource, int>? removeFromRelationship = null) : base(options,
        loggerFactory, getAll, getById, getSecondary, getRelationship, create, addToRelationship, update,
        setRelationship, delete, removeFromRelationship) { }
}
