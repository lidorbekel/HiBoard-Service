using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;
using HiBoard.Persistence;
using Microsoft.AspNetCore.Http;

namespace HiBoard.Application.Services
{
    public class CompaniesService
    {
        private readonly CompaniesRepository _repository;

        public CompaniesService(CompaniesRepository repository)
        {
            _repository = repository;
        }


        public async Task<CompanyDto?> CreateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken)
        {
            return await _repository.CreateCompanyAsync(companyDto, cancellationToken);
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int companyId, CancellationToken cancellationToken)
        {
            return await _repository.GetCompanyByIdAsync(companyId, cancellationToken);
        }

        public async Task<CompanyDto> UpdateCompanyAsync(int companyId, CompanyDto companyDto, CancellationToken cancellationToken)
        {
            return await _repository.UpdateCompanyAsync(companyId, companyDto, cancellationToken);
        }
    }
}
