using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;

public class User : ModelBase<User, int>
{
    public override int Id { get; protected set; }

    public string UserName { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public UserRoles Role { get; set; }

    public UserDepartments Department { get; set; }

    public DateTime CreationDate { get; set; }

    public bool IsDeleted { get; set; }

    public User(string userName) => UserName = userName;
}
