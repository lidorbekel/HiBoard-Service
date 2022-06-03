using HiBoard.Application.Services;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HiBoard.Service.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/companies/{companyId}/department/{department}/templates")]
public class TemplatesController : ControllerBase
{
    private readonly TemplatesService _service;

    public TemplatesController(TemplatesService service)
    {
        _service = service;
    }

    [SwaggerOperation("Get All Templates")]
    [HttpGet]
    public async Task<IActionResult> GetTemplates(int companyId, string department, CancellationToken cancellationToken)
    {
        var templates = await _service.GetAllTemplates(companyId, department, cancellationToken);
        var response = new HiBoardResponse<IReadOnlyCollection<TemplateDto>>(templates);
        
        return Ok(response);
    }

    [SwaggerOperation("Get Template By Id")]
    [HttpGet("{templateId}")]
    public async Task<IActionResult> GetTemplateById(int templateId, CancellationToken cancellationToken)
    {
        var template = await _service.GetTemplateById(templateId, cancellationToken);
        var response = new HiBoardResponse<TemplateDto>(template);
        
        return Ok(response);
    }

    [SwaggerOperation("Create Template")]
    [HttpPost]
    public async Task<IActionResult> CreateTemplate(int companyId, string department, [FromBody] TemplateDto template, CancellationToken cancellationToken)
    {
        var createdTemplate = await _service.CreateTemplate(companyId, department, template, cancellationToken);
        var response = new HiBoardResponse<TemplateDto>(createdTemplate);
        
        return Ok(response);
    }
    
    [SwaggerOperation("Update Template")]
    [HttpPatch("{templateId}")]
    public async Task<IActionResult> UpdateTemplate(int templateId, [FromBody] TemplateDto template, CancellationToken cancellationToken)
    {
        var createdTemplate = await _service.UpdateTemplate(templateId, template, cancellationToken);
        var response = new HiBoardResponse<TemplateDto>(createdTemplate);
        
        return Ok(response);
    }
    
    [SwaggerOperation("Delete Template")]
    [HttpDelete("{templateId}")]
    public async Task<IActionResult> DeleteTemplate(int templateId, CancellationToken cancellationToken)
    {
        await _service.DeleteTemplate(templateId, cancellationToken);
        
        return NoContent();
    }


}

