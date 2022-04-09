using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;

namespace HiBoard.Application.Services
{
    public class UsersService
    {
        private readonly UsersRepository _repository;

        public UsersService(UsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> GetUserInfoAsync(int userId)
        {
            return await _repository.GetByIdAsync(userId);
        }
    }
}