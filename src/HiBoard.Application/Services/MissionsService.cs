using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Services
{
    public class MissionsService
    {
        private readonly MissionsRepository _repository;

        public MissionsService(MissionsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<MissionDto>> GetMissionsAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetListAsync(cancellationToken);
        }

        public async Task<MissionDto> GetMissionAsync(int missionId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(missionId, cancellationToken);
        }

        public async Task<MissionDto> CreateMissionAsync(MissionDto missionDto, CancellationToken cancellationToken)
        {
            return await _repository.CreateAsync(missionDto, cancellationToken);
        }

        public async Task<MissionDto> UpdateMissionAsync(int missionId, MissionDto missionDto, CancellationToken cancellationToken)
        {
            return await _repository.UpdateAsync(missionId, missionDto, cancellationToken);
        }

        public async Task DeleteMissionAsync(int missionId, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(missionId, cancellationToken);
        }
    }
}