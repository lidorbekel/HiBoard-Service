using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly CompaniesService _service;

    public CompaniesController(CompaniesService service)
    {
        _service = service;
    }

    [SwaggerOperation("Create Company")]
    [HttpPost]
    public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyDto companyDto, CancellationToken cancellationToken)
    {
        var company = await _service.CreateCompanyAsync(companyDto, cancellationToken);
        var response = new HiBoardResponse<CompanyDto>(company);

        return Ok(response);
    }

    [SwaggerOperation("Get Company")]
    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetCompanyByIdAsync(int companyId, CancellationToken cancellationToken)
    {
        var company = await _service.GetCompanyByIdAsync(companyId, cancellationToken);
        var response = new HiBoardResponse<CompanyDto>(company);
        
        return Ok(response);
    }

    [SwaggerOperation("Edit Company")]
    [HttpPatch("{companyId}")]
    public async Task<IActionResult> UpdateCompanyAsync(int companyId, CompanyDto companyDto,
        CancellationToken cancellationToken)
    {
        var company = await _service.UpdateCompanyAsync(companyId, companyDto, cancellationToken);
        var response = new HiBoardResponse<CompanyDto>(company);

        return Ok(response);
    }
}

