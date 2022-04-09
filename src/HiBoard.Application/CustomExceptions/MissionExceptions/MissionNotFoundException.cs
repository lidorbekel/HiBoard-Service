using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.MissionExceptions;

public class MissionNotFoundException : Exception
{
    [PublicAPI]
    public int MissionId { get; }

    public MissionNotFoundException(int missionId) : base($"Mission with id: {missionId} not found") =>
        MissionId = missionId;
}