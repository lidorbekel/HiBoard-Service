using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Models;

namespace HiBoard.Domain.DTOs;

public class TemplateDto
{
    [NotMapped]
    public int Id { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Activity> Activities { get; set; } = new List<Activity>();

    public string Department { get; set; } = string.Empty;
}