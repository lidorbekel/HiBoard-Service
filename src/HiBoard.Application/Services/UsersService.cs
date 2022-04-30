using System.Net;
using System.Text.Json.Serialization;
using AutoMapper;
using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Requests;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;

namespace HiBoard.Application.Services
{
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

        public async Task<IReadOnlyCollection<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetListAsync(cancellationToken);
        }

        public async Task<UserDto> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(userId, cancellationToken);
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            return await _repository.CreateAsync(userDto, cancellationToken);
        }

        public async Task<UserDto> UpdateUserAsync(int userId, PatchUser patchUser, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(userId, cancellationToken);

            if (!string.IsNullOrWhiteSpace(patchUser.NewPassword))
            {
                var api = "AIzaSyBD-MmZTd6BvQWX6NDBCVQimE9iib29PUA";
                var httpClient = new RestClient($"https://identitytoolkit.googleapis.com");
                var request = new RestRequest($"/v1/accounts:update?key={api}", Method.Post);

                var idToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                request.AddJsonBody(new
                {
                    idToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1],
                    password = patchUser.NewPassword,
                    returnSecureToken = false
                });
                request.AddHeader("Content-Type", "application/json");
                var response = await httpClient.ExecuteAsync(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new HttpRequestException($"Failed to update user at Firebase, StatusCode: {response.StatusCode}, content: {response.Content}");
                }
            }

            var userDto = _mapper.Map<UserDto>(patchUser);
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
}