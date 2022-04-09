using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.UsersException;

public class UserNotFoundException : Exception
{
    [PublicAPI]
    public int UserId { get; }

    public UserNotFoundException(int userId) : base($"User with id: {userId} not found") =>
        UserId = userId;
}