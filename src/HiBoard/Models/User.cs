using HiBoard.Enums;

namespace HiBoard.Models;

public class User : Entity<User, int>
{
    public override int Id { get; protected set; }
    public string UserName { get; set; }
    public string Title { get; set; }
    public UserRoles Role { get; set; }
    public UserDepartments Department { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsDeleted { get; set; }

    public User(string userName, string title, UserRoles role,
        UserDepartments department)
    {
        UserName = userName;
        Title = title;
        Role = role;
        Department = department;
    }
}
