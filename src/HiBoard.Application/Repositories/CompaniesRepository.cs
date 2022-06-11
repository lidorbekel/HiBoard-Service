using AutoMapper;
using HiBoard.Application.CustomExceptions.CompanyExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories;

public class CompaniesRepository
{
    private readonly HiBoardDbContext _context;
    private readonly IMapper _mapper;

    public CompaniesRepository(HiBoardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyDto companyDto, CancellationToken cancellationToken)
    {
        var company = _mapper.Map<Company>(companyDto);

        await _context.Companies.AddAsync(company, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<CompanyDto> GetCompanyByIdAsync(int companyId, CancellationToken cancellationToken)
    {
        var company = await _context.Companies.Include(c => c.Users)
            .FirstOrDefaultAsync(company => company.Id == companyId, cancellationToken);

        if (company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<CompanyDto> UpdateCompanyAsync(int companyId, CompanyDto companyDto,
        CancellationToken cancellationToken)
    {
        var company = await _context.Companies.FindAsync(new object?[] {companyId}, cancellationToken);
        if (company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        company.Name = companyDto.Name;
        company.Departments = companyDto.Departments;
        company.Description = companyDto.Description;
        company.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}