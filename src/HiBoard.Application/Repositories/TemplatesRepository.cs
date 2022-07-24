using AutoMapper;
using HiBoard.Application.CustomExceptions.TemplateExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories;

public class TemplatesRepository : ITemplatesRepository
{
    private readonly HiBoardDbContext _context;
    private readonly IMapper _mapper;

    public TemplatesRepository(HiBoardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<TemplateDto>> GetAllTemplates(int companyId, string department, CancellationToken cancellationToken)
    {
        var templates = await _context.Templates
            .Include(x => x.Activities)
            .Where(t => t.CompanyId == companyId && t.Department == department)
            .AsNoTracking().ToListAsync(cancellationToken);
        
        return _mapper.Map<List<TemplateDto>>(templates);
    }

    public async Task<TemplateDto> GetTemplateDtoById(int templateId, CancellationToken cancellationToken)
    {
        var template = await _context.Templates.AsNoTracking().Include(x=> x.Activities)
            .FirstOrDefaultAsync(template => template.Id == templateId, cancellationToken: cancellationToken);
        
        return _mapper.Map<TemplateDto>(template);
    }

    public async Task<Template> GetTemplateById(int templateId, CancellationToken cancellationToken)
    {
        var template = await _context.Templates.Include(x => x.Activities)
            .FirstOrDefaultAsync(template => template.Id == templateId, cancellationToken: cancellationToken);

        if (template == null)
        {
            throw new TemplateNotFoundException(templateId);
        }
        
        return template;
    }

    public async Task<TemplateDto> CreateTemplate(int companyId, string department, TemplateDto templateDto, CancellationToken cancellationToken)
    {
        var templateEntity = _mapper.Map<Template>(templateDto);
        templateEntity.CompanyId = companyId;
        templateEntity.Department = department;
        
        await _context.Templates.AddAsync(templateEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TemplateDto>(templateEntity);
    }

    public async Task<TemplateDto> UpdateTemplate(int templateId, TemplateDto templateDto, CancellationToken cancellationToken)
    {
        var template = await GetTemplateById(templateId, cancellationToken);

        if (template == null)
        {
            throw new TemplateNotFoundException(templateId);
        }

        template.Name = templateDto.Name;
        template.UpdatedAt = DateTime.UtcNow;
        var templateDtoAsTemplateObject = _mapper.Map<Template>(templateDto);
        var activitiesToRemove = template.Activities.Except(templateDtoAsTemplateObject.Activities).ToList();
        foreach (var activity in activitiesToRemove)
        {
            template.Activities.Remove(activity);
        }

        _context.Templates.Update(template);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TemplateDto>(template);
    }

    public async Task DeleteTemplate(int templateId, CancellationToken cancellationToken)
    {
        var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == templateId, cancellationToken: cancellationToken);
        if (template == null)
        {
            throw new TemplateNotFoundException(templateId);
        }
        
        template.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddActivityToTemplates(AddActivityToTemplates request, CancellationToken cancellationToken)
    {
        var activityEntity = _mapper.Map<Activity>(request.Activity);
        if (request.TemplatesIds != null)
        {
            foreach (var id in request.TemplatesIds)
            {
                var template = await GetTemplateById(id, cancellationToken);
                if (template.Activities.All(a => a.Id != activityEntity.Id))
                {
                    template.Activities.Add(activityEntity);
                }
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}

