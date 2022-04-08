using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.Models;
using HiBoard.Persistence;

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
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Id == userId);
            return user;
        }
    }
}
