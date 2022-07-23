using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Repositories;

public interface IActivitiesRepository
{
    Task<IReadOnlyCollection<ActivityDto>> GetListAsync(CancellationToken cancellationToken);
    Task<ActivityDto> GetByIdAsync(int activityId, CancellationToken cancellationToken);
    Task<ActivityDto> CreateAsync(ActivityDto activityDto, CancellationToken cancellationToken);
    Task<ActivityDto> UpdateAsync(int activityId, ActivityDto activityDto, CancellationToken cancellationToken);
    Task DeleteAsync(int activityId, CancellationToken cancellationToken);
}