using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.UsersExceptions;

public class UserAlreadyExistsException : Exception
{
    [PublicAPI]
    public string? UserName { get; }

    public UserAlreadyExistsException(string? userName) : base($"User with user name: {userName} already exists") =>
        UserName = userName;
}