using System.ComponentModel.DataAnnotations.Schema;

namespace HiBoard.Domain.DTOs;

public class ActivityDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Tag { get; set; } = string.Empty;

    public int Week { get; set; }

    public TimeSpan TimeEstimation { get; set; }
}