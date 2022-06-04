using System.ComponentModel.DataAnnotations.Schema;

namespace HiBoard.Domain.DTOs;

public class UserDto
{
    public int Id { get; protected set; }
    
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public string NewPassword { get; set; } = string.Empty;

    public int CompanyId { get; set; }
    
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Role { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;
    
    public int ManagerId { get; set; }
}