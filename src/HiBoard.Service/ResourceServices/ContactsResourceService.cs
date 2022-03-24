using AutoMapper;
using HiBoard.Service.Data;
using HiBoard.Service.Resources;
using JetBrains.Annotations;
using JsonApiDotNetCore.Configuration;
using JsonApiDotNetCore.Hooks;
using JsonApiDotNetCore.Middleware;
using JsonApiDotNetCore.Queries;
using JsonApiDotNetCore.Repositories;
using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Services;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Service.ResourceServices;

[UsedImplicitly]
public class ContactsResourceService : JsonApiResourceService<ContactResource>
{
    private readonly HiBoardDbContext _db;
    private readonly IMapper _mapper;

    public ContactsResourceService(IResourceRepositoryAccessor repositoryAccessor,
        IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options,
        ILoggerFactory loggerFactory, IJsonApiRequest request,
        IResourceChangeTracker<ContactResource> resourceChangeTracker, IResourceHookExecutorFacade hookExecutor,
        HiBoardDbContext db, IMapper mapper) :
        base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request,
            resourceChangeTracker, hookExecutor)
    {
        _db = db;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<ContactResource>> GetAsync(CancellationToken cancellationToken)
    {
        var allContacts = _db.Contacts.AsNoTracking();
        var contactResources = await _mapper.ProjectTo<ContactResource>(allContacts).ToListAsync(cancellationToken);

        return contactResources;
    }

    public override async Task<ContactResource> GetAsync(int id, CancellationToken cancellationToken)
    {
        var contact = await _db.Contacts.FindAsync(id);

        return _mapper.Map<ContactResource>(contact);
    }
}
