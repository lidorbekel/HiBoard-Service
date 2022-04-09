using AutoMapper;
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

        public async Task<MissionDto> GetByIdAsync(int missionId)
        {
            var mission = (await _context.Missions.AsNoTracking().SingleOrDefaultAsync(mission => mission.Id == missionId))!;

            var result = _mapper.Map<MissionDto>(mission);

            return result;
        }
    }
}
