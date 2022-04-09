using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories
{
    public class UsersRepository
    {
        private readonly HiBoardDbContext _context;

        public UsersRepository(HiBoardDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return (await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Id == userId))!;
        }
    }
}
