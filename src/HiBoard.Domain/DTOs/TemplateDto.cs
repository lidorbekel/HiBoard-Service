using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Models;

namespace HiBoard.Domain.DTOs;

public class TemplateDto
{
    [NotMapped] public int Id { get; protected set; }

    public string Name { get; set; } = string.Empty;
    
    public List<Activity> Activities { get; set; } = new();
    
    public int CompanyId { get; set; }

    public string Department { get; set; } = string.Empty;
}