using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Repositories
{
    public interface IUsersRepository
    {
        Task<IReadOnlyCollection<UserDto>> GetUserEmployeesAsync(int userId, CancellationToken cancellationToken);
        Task<UserDto> GetByIdAsync(int userId, CancellationToken cancellationToken);
        Task<UserDto> GetByEmail(string? email, CancellationToken cancellationToken);
        Task<UserDto> CreateAsync(UserDto userDto, int managerId, CancellationToken cancellationToken);
        Task<UserDto> UpdateAsync(int userId, UserDto userDto, CancellationToken cancellationToken);
        Task DeleteAsync(int userId, CancellationToken cancellationToken);
    }
}