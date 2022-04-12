using AutoMapper;
using HiBoard.Application.CustomExceptions.ActivityExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories
{
    public class ActivitiesRepository
    {
        private readonly HiBoardDbContext _context;
        private readonly IMapper _mapper;

        public ActivitiesRepository(HiBoardDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<ActivityDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var activities = await _context.Activities.AsNoTracking().ToListAsync(cancellationToken);

            return _mapper.Map<List<ActivityDto>>(activities);
        }

        public async Task<ActivityDto> GetByIdAsync(int activityId, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(new object?[] { activityId }, cancellationToken);
            if (activity == null)
            {
                throw new ActivityNotFoundException(activityId);
            }

            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<ActivityDto> CreateAsync(ActivityDto activityDto, CancellationToken cancellationToken)
        {
            var activity = _mapper.Map<Activity>(activityDto);
            
            await _context.Activities.AddAsync(activity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task<ActivityDto> UpdateAsync(int activityId, ActivityDto activityDto, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(new object?[] { activityId }, cancellationToken);
            if (activity == null)
            {
                throw new ActivityNotFoundException(activityId);
            }

            activity = _mapper.Map<Activity>(activityDto);

            _context.Activities.Update(activity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ActivityDto>(activity);
        }

        public async Task DeleteAsync(int activityId, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(new object?[] { activityId }, cancellationToken);
            if (activity == null)
            {
                throw new ActivityNotFoundException(activityId);
            }

            activity.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}