using AutoMapper;
using HiBoard.Application.Repositories;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;

namespace HiBoard.Application.Services;

public class TemplatesService
{
    private readonly IMapper _mapper;
    private readonly TemplatesRepository _repository;

    public TemplatesService(IMapper mapper, TemplatesRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }


    public async Task<IReadOnlyCollection<TemplateDto>> GetAllTemplates(int companyId, string department,
        CancellationToken cancellationToken)
    {
        var templates = await _repository.GetAllTemplates(companyId, department, cancellationToken);

        return templates;
    }

    public async Task<TemplateDto?> GetTemplateById(int templateId, CancellationToken cancellationToken)
    {
        var template = await _repository.GetTemplateDtoById(templateId, cancellationToken);
        return template;
    }

    public async Task<TemplateDto?> CreateTemplate(int companyId, string department, TemplateDto templateDto,
        CancellationToken cancellationToken)
    {
        return await _repository.CreateTemplate(companyId, department, templateDto, cancellationToken);
    }

    public async Task<TemplateDto?> UpdateTemplate(int templateId, TemplateDto templateDto,
        CancellationToken cancellationToken)
    {
        return await _repository.UpdateTemplate(templateId, templateDto, cancellationToken);
    }

    public async Task DeleteTemplate(int templateId, CancellationToken cancellationToken)
    {
        await _repository.DeleteTemplate(templateId, cancellationToken);
    }

    public async Task AddActivityToTemplates(AddActivityToTemplates request, CancellationToken cancellationToken)
    {
        await _repository.AddActivityToTemplates(request, cancellationToken);
    }
}