using AutoMapper;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories
{
    public class UsersRepository
    {
        private readonly HiBoardDbContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(HiBoardDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(int userId)
        {
            var user = (await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Id == userId))!;
            var result = _mapper.Map<UserDto>(user);

            return result;
        }
    }
}
