namespace HiBoard.Domain.Models;

public class Template : ModelBase<Template,int>
{
    public override int Id { get; protected set; }
    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public override DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Name { get; set; } = string.Empty;
    public List<Activity> Activities { get; set; } = new ();
    public int CompanyId { get; set; }
    public string Department { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}