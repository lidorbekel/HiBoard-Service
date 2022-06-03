using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiBoard.Application.CustomExceptions.TemplateExceptions;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiBoard.Application.Repositories;

public class TemplatesRepository
{
    private readonly HiBoardDbContext _context;
    private readonly IMapper _mapper;

    public TemplatesRepository(HiBoardDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<TemplateDto>> GetAllTemplates(int companyId, string department,
        CancellationToken cancellationToken)
    {
        var templates = await _context.Templates.Where(t => t.CompanyId == companyId && t.Department == department)
            .AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<TemplateDto>>(templates);
    }

    public async Task<TemplateDto> GetTemplateById(int templateId, CancellationToken cancellationToken)
    {
        var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == templateId, cancellationToken: cancellationToken);
        return _mapper.Map<TemplateDto>(template);
    }

    public async Task<TemplateDto> CreateTemplate(int companyId, string department, TemplateDto template, CancellationToken cancellationToken)
    {
        var templateEntity = _mapper.Map<Template>(template);
        await _context.Templates.AddAsync(templateEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return template = _mapper.Map<TemplateDto>(templateEntity);
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
}