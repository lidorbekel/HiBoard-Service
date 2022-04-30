using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.UsersExceptions;

public class UserAlreadyExistsException : Exception
{
    [PublicAPI]
    public string? Email { get; }

    public UserAlreadyExistsException(string? email) : base($"User with user name: {email} already exists") =>
        Email = email;
}