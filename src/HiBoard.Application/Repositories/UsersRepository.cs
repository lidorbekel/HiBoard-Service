using AutoMapper;
using HiBoard.Application.CustomExceptions.UsersExceptions;
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

        public async Task<IReadOnlyCollection<UserDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);

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

        public async Task<UserDto> CreateAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            var isUserExists = await _context.Users.AnyAsync(x => x.UserName == userDto.UserName, cancellationToken);
            if (isUserExists)
            {
                throw new UserAlreadyExistsException(userDto.UserName);
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

            user = _mapper.Map<User>(userDto);
            
            _context.Users.Update(user);
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
    }
}