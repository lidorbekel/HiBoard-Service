using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Services;

public class UserActivitiesService
{
    private readonly UserActivitiesRepository _repository;

    public UserActivitiesService(UserActivitiesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<UserActivityDto>> GetActivitiesAsync(int userId, CancellationToken cancellationToken)
    {
        return await _repository.GetListAsync(userId, cancellationToken);
    }
    
    public async Task<UserActivityDto> GetUserActivityAsync(int activityId, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(activityId, cancellationToken);
    }
    
    public async Task<UserActivityDto> UpdateUserActivityAsync(int activityId, UserActivityDto activityDto, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(activityId, activityDto, cancellationToken);
    }
    
    public async Task<UserActivityDto> CreateUserActivityAsync(UserActivityDto activityDto, CancellationToken cancellationToken)
    {
        return await _repository.CreateAsync(activityDto, cancellationToken);
    }
    
    public async Task DeleteUserActivityAsync(int activityId, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(activityId, cancellationToken);
    }
}