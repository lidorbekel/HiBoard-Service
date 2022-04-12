using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.ActivityExceptions;

public class ActivityNotFoundException : Exception
{
    [PublicAPI]
    public int ActivityId { get; }

    public ActivityNotFoundException(int activityId) : base($"Activity with id: {activityId} not found") =>
        ActivityId = activityId;
}