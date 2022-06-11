using HiBoard.Domain.Enums;

namespace HiBoard.Domain.DTOs;

public class UserActivityDto
{

    public int Id { get; set; }

    public Status Status { get; set; }
    
    public bool? IsOnTime { get; set; }
    
    public ActivityDto? Activity { get; set; }
}