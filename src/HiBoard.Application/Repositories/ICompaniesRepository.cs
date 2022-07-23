using HiBoard.Domain.DTOs;

namespace HiBoard.Application.Repositories;

public interface ICompaniesRepository
{
    Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken);
    Task<CompanyDto> GetCompanyByIdAsync(int companyId, CancellationToken cancellationToken);
    Task<CompanyDto> UpdateCompanyAsync(int companyId, CompanyDto companyDto, CancellationToken cancellationToken);
}