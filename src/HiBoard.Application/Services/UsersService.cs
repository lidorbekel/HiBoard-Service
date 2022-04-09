using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Services
{
    public class UsersService
    {
        private readonly UsersRepository _repository;

        public UsersService(UsersRepository repository)
        {
            _repository = repository;
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

        public async Task<UserDto> UpdateUserAsync(int userId, UserDto userDto, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(userId, userDto, cancellationToken);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(userId, cancellationToken);
        }
    }
}