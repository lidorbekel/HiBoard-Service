using AutoMapper;
using HiBoard.Application.CustomExceptions.MissionExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories
{
    public class MissionsRepository
    {
        private readonly HiBoardDbContext _context;
        private readonly IMapper _mapper;

        public MissionsRepository(HiBoardDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<MissionDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var missions = await _context.Missions.AsNoTracking().ToListAsync(cancellationToken);

            return _mapper.Map<List<MissionDto>>(missions);
        }

        public async Task<MissionDto> GetByIdAsync(int missionId, CancellationToken cancellationToken)
        {
            var mission = await _context.Missions.FindAsync(new object?[] { missionId }, cancellationToken);
            if (mission == null)
            {
                throw new MissionNotFoundException(missionId);
            }

            return _mapper.Map<MissionDto>(mission);
        }

        public async Task<MissionDto> CreateAsync(MissionDto missionDto, CancellationToken cancellationToken)
        {
            var mission = _mapper.Map<Mission>(missionDto);
            
            await _context.Missions.AddAsync(mission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MissionDto>(mission);
        }

        public async Task<MissionDto> UpdateAsync(int missionId, MissionDto missionDto, CancellationToken cancellationToken)
        {
            var mission = await _context.Missions.FindAsync(new object?[] { missionId }, cancellationToken);
            if (mission == null)
            {
                throw new MissionNotFoundException(missionId);
            }

            mission = _mapper.Map<Mission>(missionDto);

            _context.Missions.Update(mission);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MissionDto>(mission);
        }

        public async Task DeleteAsync(int missionId, CancellationToken cancellationToken)
        {
            var mission = await _context.Missions.FindAsync(new object?[] { missionId }, cancellationToken);
            if (mission == null)
            {
                throw new MissionNotFoundException(missionId);
            }

            mission.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}