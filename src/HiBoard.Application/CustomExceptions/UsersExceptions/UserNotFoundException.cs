using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.UsersExceptions;

public class UserNotFoundException : Exception
{
    [PublicAPI]
    public int UserId { get; }

    public string? Email { get; }

    public UserNotFoundException(int userId) : base($"User with id: {userId} not found") =>
        UserId = userId;

    public UserNotFoundException(string? email) : base($"User with email: {email} not found") =>
        Email = email;
}