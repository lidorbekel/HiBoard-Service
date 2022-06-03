using AutoMapper;
using HiBoard.Application.CustomExceptions.ActivityExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
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

    public async Task<IReadOnlyCollection<UserActivityDto>> GetListAsync(int userId, CancellationToken cancellationToken)
    {
        var activities = await _context.UserActivities
            .Include(x=> x.Activity)
            .Where(x => x.UserId == userId).AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<UserActivityDto>>(activities);
    }
        
    public async Task<UserActivityDto> GetByIdAsync(int activityId, CancellationToken cancellationToken)
    {
        var userActivity = await _context.UserActivities.FindAsync(new object?[] { activityId }, cancellationToken);
        if (userActivity == null)
        {
            throw new ActivityNotFoundException(activityId);
        }

        return _mapper.Map<UserActivityDto>(userActivity);
    }

    public async Task<UserActivityDto> UpdateAsync(int activityId, UserActivityDto activityDto, CancellationToken cancellationToken)
    {
        var activity = await _context.UserActivities.FindAsync(new object?[] { activityId }, cancellationToken);
        if (activity == null)
        {
            throw new ActivityNotFoundException(activityId);
        }

        activity = _mapper.Map<UserActivity>(activityDto);

        _context.UserActivities.Update(activity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserActivityDto>(activity);
    }
        
    public async Task<UserActivityDto> CreateAsync(UserActivityDto activityDto, CancellationToken cancellationToken)
    {
        var activity = _mapper.Map<UserActivity>(activityDto);
            
        await _context.UserActivities.AddAsync(activity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserActivityDto>(activity);
    }
        
    public async Task DeleteAsync(int activityId, CancellationToken cancellationToken)
    {
        var activity = await _context.UserActivities.FindAsync(new object?[] { activityId }, cancellationToken);
        if (activity == null)
        {
            throw new ActivityNotFoundException(activityId);
        }

        activity.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }
}