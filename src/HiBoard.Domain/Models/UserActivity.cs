namespace HiBoard.Domain.Models;

public class UserActivity : ModelBase<UserActivity,int>
{
    public override int Id { get; protected set; }

    public User? User { get; set; }

    public Activity? Activity { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime? CreatedAt { get; set; } 

    public DateTime? UpdatedAt { get; set; }
}