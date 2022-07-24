using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Models;

namespace HiBoard.Domain.DTOs;

public class TemplateDto
{
    [NotMapped] public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public List<ActivityDto> Activities { get; set; } = new();
    
    public int CompanyId { get; set; }

    public string Department { get; set; } = string.Empty;
}