using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;

public class UserActivity : ModelBase<UserActivity,int>
{
    public override int Id { get; protected set; }

    public User? User { get; set; }

    public Activity? Activity { get; set; }

    public Status Status { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public override DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}