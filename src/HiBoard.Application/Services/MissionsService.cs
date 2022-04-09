using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<MissionDto> GetByIdAsync(int missionId)
        {
            return await _repository.GetByIdAsync(missionId);
        }
    }
}
