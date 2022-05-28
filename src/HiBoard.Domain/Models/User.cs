using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;

public class User : ModelBase<User, int>
{
    public override int Id { get; protected set; }

    public override DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    
    public override DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public string Email { get; set; }

    public string? FirstName { get; set; } = string.Empty;

    public string? LastName { get; set; } = string.Empty;

    public UserRole Role { get; set; } 

    public string Department { get; set; } = string.Empty;

    public ICollection<UserActivity>? UserActivities { get; set; }

    public bool IsDeleted { get; set; }
    
    public int CompanyId { get; set; }

    [ForeignKey(nameof(CompanyId))]
    public Company? Company { get; set; }
    
    public int ManagerId { get; set; }

    public User(string email) => Email = email;
}