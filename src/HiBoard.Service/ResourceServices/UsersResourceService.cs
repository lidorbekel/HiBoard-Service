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
public class UsersResourceService : JsonApiResourceService<UserResource>
{
    private readonly HiBoardDbContext _db;
    private readonly IMapper _mapper;

    public UsersResourceService(IResourceRepositoryAccessor repositoryAccessor,
        IQueryLayerComposer queryLayerComposer, IPaginationContext paginationContext, IJsonApiOptions options,
        ILoggerFactory loggerFactory, IJsonApiRequest request,
        IResourceChangeTracker<UserResource> resourceChangeTracker, IResourceHookExecutorFacade hookExecutor,
        HiBoardDbContext db, IMapper mapper) :
        base(repositoryAccessor, queryLayerComposer, paginationContext, options, loggerFactory, request,
            resourceChangeTracker, hookExecutor)
    {
        _db = db;
        _mapper = mapper;
    }

    public override async Task<IReadOnlyCollection<UserResource>> GetAsync(CancellationToken cancellationToken)
    {
        var users = _db.Users.AsNoTracking();
        var userResources = await _mapper.ProjectTo<UserResource>(users).ToListAsync(cancellationToken);

        return userResources;
    }

    public override async Task<UserResource> GetAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FindAsync(id, cancellationToken);

        return _mapper.Map<UserResource>(user);
    }
}
