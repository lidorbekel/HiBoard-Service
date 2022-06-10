using AutoMapper;
using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Enums;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;

namespace HiBoard.Application.Services;

public class UserActivitiesService
{
    private readonly UserActivitiesRepository _repository;
    private readonly IMapper _mapper;
    private readonly TemplatesService _templatesService;


    public UserActivitiesService(UserActivitiesRepository repository, IMapper mapper, TemplatesService templatesService)
    {
        _repository = repository;
        _mapper = mapper;
        _templatesService = templatesService;
    }

    public async Task<IReadOnlyCollection<UserActivityDto>> GetActivitiesAsync(int userId, CancellationToken cancellationToken)
    {
        return await _repository.GetListAsync(userId, cancellationToken);
    }
    
    public async Task<UserActivityDto> GetUserActivityAsync(int activityId, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(activityId, cancellationToken);
    }
    
    public async Task<UserActivityDto> UpdateUserActivityAsync(int activityId, UserActivityDto userActivityDto, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(activityId, userActivityDto, cancellationToken);
    }
    
    public async Task<UserActivityDto> CreateUserActivityAsync(int userId,UserActivityDto userActivityDto, CancellationToken cancellationToken)
    {
        return await _repository.CreateAsync(userId,userActivityDto, cancellationToken);
    }
    
    public async Task DeleteUserActivityAsync(int activityId, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(activityId, cancellationToken);
    }

    public async Task CreateUserActivityByActivityAsync(int userId, Activity activity, CancellationToken cancellationToken)
    {
        await _repository.CreateUserActivityByActivityAsync(userId,activity, cancellationToken);
        
    }

    public async Task AssignTemplateToUser(int userId, int templateId, CancellationToken cancellationToken)
    {
        Template template = _mapper.Map<Template>(await _templatesService.GetTemplateById(templateId, cancellationToken));

        if (template is not null)
        {
            foreach (var activity in template.Activities)
            {
                await CreateUserActivityByActivityAsync(userId, activity, cancellationToken);
            }
        }
    }
}