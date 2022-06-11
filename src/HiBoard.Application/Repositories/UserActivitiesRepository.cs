using AutoMapper;
using HiBoard.Application.CustomExceptions.ActivityExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Enums;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories;

public class UserActivitiesRepository
{
    private readonly HiBoardDbContext _context;
    private readonly IMapper _mapper;

    public UserActivitiesRepository(HiBoardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<UserActivityDto>> GetListAsync(int userId,
        CancellationToken cancellationToken)
    {
        var activities = await _context.UserActivities
            .Include(x => x.Activity)
            .Where(x => x.UserId == userId).AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<UserActivityDto>>(activities);
    }

    public async Task<UserActivityDto> GetByIdAsync(int userActivityId, CancellationToken cancellationToken)
    {
        var userActivity = await _context.UserActivities.FindAsync(new object?[] {userActivityId}, cancellationToken);
        if (userActivity == null)
        {
            throw new ActivityNotFoundException(userActivityId);
        }

        return _mapper.Map<UserActivityDto>(userActivity);
    }

    public async Task<UserActivityDto> UpdateAsync(int userActivityId, UserActivityDto userActivityDto,
        CancellationToken cancellationToken)
    {
        var userActivity = await _context.UserActivities.Include(x => x.Activity)
            .FirstOrDefaultAsync(userActivity => userActivity.Id == userActivityId, cancellationToken);

        if (userActivity == null)
        {
            throw new ActivityNotFoundException(userActivityId);
        }

        userActivity.Status = userActivityDto.Status;
        userActivity.UpdatedAt = DateTime.UtcNow;

        _context.UserActivities.Update(userActivity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserActivityDto>(userActivity);
    }

    public async Task<UserActivityDto> CreateAsync(int userId, UserActivityDto userActivityDto,
        CancellationToken cancellationToken)
    {
        var userActivity = _mapper.Map<UserActivity>(userActivityDto);
        userActivity.UserId = userId;
        await _context.UserActivities.AddAsync(userActivity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserActivityDto>(userActivity);
    }

    public async Task DeleteAsync(int userActivityId, CancellationToken cancellationToken)
    {
        var activity = await _context.UserActivities.FindAsync(new object?[] {userActivityId}, cancellationToken);
        if (activity == null)
        {
            throw new ActivityNotFoundException(userActivityId);
        }

        activity.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateUserActivityByActivityAsync(int userId, Activity activity,
        CancellationToken cancellationToken)
    {
        var userActivity = new UserActivity
        {
            ActivityId = activity.Id,
            UserId = userId,
            IsDeleted = false,
            Status = Status.Backlog
        };

        await _context.UserActivities.AddAsync(userActivity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}