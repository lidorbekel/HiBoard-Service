namespace HiBoard.Domain.Models;

public class Company : ModelBase<Company, int>
{
    public override int Id { get; protected set; }

    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    
    public override DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string? Name { get; set; }

    public ICollection<User>? Users { get; set; }

    public string Admin { get; set; } = string.Empty;

    public List<string> Departments { get; set; } = new();

    public string Description { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }
}