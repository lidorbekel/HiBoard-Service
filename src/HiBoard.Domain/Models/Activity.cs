using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;

public class Activity : ModelBase<Activity, int>
{
    public override int Id { get; protected set; }

    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    
    public override DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Tag { get; set; }

    [NotMapped] //TODO NEED TO MAP THIS
    public ICollection<int>? DependencyIds { get; set; }

    public ICollection<UserActivity>? UserActivities { get; set; }

    public TimeSpan TimeEstimation { get; set; }

    public bool IsDeleted { get; set; }
}