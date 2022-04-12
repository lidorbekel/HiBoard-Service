using System.Data.Entity;
using System.Text.Json.Serialization;
using AutoMapper;
using HiBoard.Application.CustomExceptions.UsersExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Persistence;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HiBoard.Application.Services;

public class UserInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HiBoardDbContext _context;
    private readonly IMapper _mapper;

    public UserInfoService(IHttpContextAccessor contextAccessor, HiBoardDbContext context, IMapper mapper)
    {
        _httpContextAccessor = contextAccessor;
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserInfo(CancellationToken cancellationToken)
    {
        var fireBaseIdentities = _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == "firebase")!.Value;

        var fireBase = JsonConvert.DeserializeObject<FireBase>(fireBaseIdentities!);
        if (fireBase == null)
        {
            throw new Exception("Error while trying to get User Info");
        }

        var fireBaseUserEmail = fireBase.Identities.Email.FirstOrDefault();
        var user = _context.Users.FirstOrDefault(x => x.UserName == fireBaseUserEmail);

        if (user == null)
        {
            throw new Exception($"User with email: {fireBaseUserEmail} not found");
        }
        
        //TODO should be removed after Initializing DataBase
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}

public class Identities
{
    [UsedImplicitly]
    [JsonPropertyName("email")] 
    public List<string> Email { get; } = new();
}

public class FireBase
{
    [JsonPropertyName("identities")] 
    public Identities Identities { get; } = new();

}