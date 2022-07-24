using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;
using HiBoard.Domain.Requests;

namespace HiBoard.Application.Repositories
{
    public interface ITemplatesRepository
    {
         Task<IReadOnlyCollection<TemplateDto>> GetAllTemplates(int companyId, string department,
        CancellationToken cancellationToken);
         Task<TemplateDto> GetTemplateDtoById(int templateId, CancellationToken cancellationToken);
         Task<Template> GetTemplateById(int templateId, CancellationToken cancellationToken);
         Task<TemplateDto> CreateTemplate(int companyId, string department, TemplateDto templateDto,
        CancellationToken cancellationToken);
         Task<TemplateDto> UpdateTemplate(int templateId, TemplateDto templateDto,
        CancellationToken cancellationToken);
         Task DeleteTemplate(int templateId, CancellationToken cancellationToken);
         Task AddActivityToTemplates(AddActivityToTemplates request, CancellationToken cancellationToken);
    }
}