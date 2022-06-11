using System.Text.Json.Serialization;
using AutoMapper;
using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HiBoard.Application.Services;

public class UsersService
{
    private readonly UsersRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UsersService(UsersRepository repository, IHttpContextAccessor httpContextAccessor,  IMapper mapper)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserAsync(int userId, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(userId, cancellationToken);
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto,int managerId, CancellationToken cancellationToken)
    {
        return await _repository.CreateAsync(userDto,managerId, cancellationToken);
    }

    public async Task<UserDto> UpdateUserAsync(int userId, UserDto userDto, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(userId, userDto, cancellationToken);
    }

    public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(userId, cancellationToken);
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

        var firebaseEmail = fireBase.Identities.Email.FirstOrDefault();
        var user = await _repository.GetByEmail(firebaseEmail,cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
        
    public async Task<IReadOnlyCollection<UserDto>> GetUserEmployees(int userId, CancellationToken cancellationToken)
    {
        return await _repository.GetUserEmployeesAsync(userId, cancellationToken);
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