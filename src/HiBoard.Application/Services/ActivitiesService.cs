using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Services;

public class ActivitiesService
{
    private readonly ActivitiesRepository _repository;

    public ActivitiesService(ActivitiesRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<ActivityDto>> GetActivitiesAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetListAsync(cancellationToken);
    }

    public async Task<ActivityDto> GetActivityAsync(int activityId, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(activityId, cancellationToken);
    }

    public async Task<ActivityDto> CreateActivityAsync(ActivityDto activityDto, CancellationToken cancellationToken)
    {
        return await _repository.CreateAsync(activityDto, cancellationToken);
    }

    public async Task<ActivityDto> UpdateActivityAsync(int activityId, ActivityDto activityDto, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(activityId, activityDto, cancellationToken);
    }

    public async Task DeleteActivityAsync(int activityId, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(activityId, cancellationToken);
    }
}