using System.ComponentModel.DataAnnotations.Schema;

namespace HiBoard.Domain.DTOs;

public class ActivityDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    [NotMapped] public List<int> DependencyIds { get; set; } = new();

    public TimeSpan TimeEstimation { get; set; }
}