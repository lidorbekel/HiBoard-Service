using HiBoard.Domain.Enums;

namespace HiBoard.Domain.DTOs;

public class UserActivityDto
{

    public int Id { get; set; }

    public ActivityDto? Activity { get; set; }

    public int ActivityId { get; set; }

    public Status Status { get; set; }
}