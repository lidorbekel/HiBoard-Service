using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;

public class UserActivity : ModelBase<UserActivity,int>
{
    
    public  override int Id { get; protected set; }

    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public override DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public int ActivityId { get; set; }
    
    
    [ForeignKey(nameof(ActivityId))]
    public Activity? Activity { get; set; }

    public Status Status { get; set; }

    public bool IsDeleted { get; set; }

    public UserActivity()
    {
    }

    public UserActivity(int id, int userId, User? user, int activityId, Activity activity, Status status)
    {
        Id = id;
        UserId = userId;
        User = user;
        ActivityId = activityId;
        Activity = activity;
        Status = status;
    }
}