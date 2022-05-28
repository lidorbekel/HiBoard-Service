using System.Net;
using AutoMapper;
using HiBoard.Application.CustomExceptions.UsersExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace HiBoard.Application.Repositories
{
    public class UsersRepository
    {
        private readonly HiBoardDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersRepository(HiBoardDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IReadOnlyCollection<UserDto>> GetUserEmployeesAsync(int userId, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Where(x => x.ManagerId == userId).AsNoTracking().ToListAsync(cancellationToken);
            
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmail(string? email, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(email);
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            var isUserExists = await _context.Users.AnyAsync(x => x.Email == userDto.Email, cancellationToken);
            if (isUserExists)
            {
                throw new UserAlreadyExistsException(userDto.Email);
            }

            var api = "AIzaSyBD-MmZTd6BvQWX6NDBCVQimE9iib29PUA";
            var httpClient = new RestClient($"https://identitytoolkit.googleapis.com");
            var request = new RestRequest($"/v1/accounts:signUp?key={api}", Method.Post);

            request.AddJsonBody(new
            {
                email = userDto.Email,
                password = userDto.Password,
                returnSecureToken = true,
            });

            request.AddHeader("Content-Type", "application/json");
            var response = await httpClient.ExecuteAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException(
                    $"Failed to create user at Firebase, StatusCode: {response.StatusCode}, content: {response.Content}");
            }

            var user = _mapper.Map<User>(userDto);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(int userId, UserDto userDto, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            var userIdToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            if (!string.IsNullOrWhiteSpace(userDto.NewPassword))
            {
                await UpdateUserPasswordInFireBase(userDto.NewPassword, userIdToken, cancellationToken);
            }

            if (user.Email != userDto.Email)
            {
                await UpdateUserEmailInFireBase(userDto.Email, userIdToken, cancellationToken);
            }

            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object?[] { userId }, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(userId);
            }

            user.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        private async Task UpdateUserEmailInFireBase(string newEmail, string userIdToken, CancellationToken cancellationToken)
        {
            const string apiKey = "AIzaSyBD-MmZTd6BvQWX6NDBCVQimE9iib29PUA";
            var httpClient = new RestClient($"https://identitytoolkit.googleapis.com");
            var request = new RestRequest($"/v1/accounts:update?key={apiKey}", Method.Post);

            request.AddJsonBody(new
            {
                idToken = userIdToken,
                email = newEmail,
                returnSecureToken = false
            });

            request.AddHeader("Content-Type", "application/json");

            var response = await httpClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Failed to update user at Firebase, StatusCode: {response.StatusCode}, content: {response.Content}");
            }
        }

        private async Task UpdateUserPasswordInFireBase(string newPassword, string userIdToken, CancellationToken cancellationToken)
        {
            const string apiKey = "AIzaSyBD-MmZTd6BvQWX6NDBCVQimE9iib29PUA";
            var httpClient = new RestClient($"https://identitytoolkit.googleapis.com");
            var request = new RestRequest($"/v1/accounts:update?key={apiKey}", Method.Post);

            request.AddJsonBody(new
            {
                idToken = userIdToken,
                password = newPassword,
                returnSecureToken = false
            });

            request.AddHeader("Content-Type", "application/json");

            var response = await httpClient.ExecuteAsync(request, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Failed to update user at Firebase, StatusCode: {response.StatusCode}, content: {response.Content}");
            }
        }
    }
}