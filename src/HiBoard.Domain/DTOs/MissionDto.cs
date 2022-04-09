using HiBoard.Domain.Enums;

namespace HiBoard.Domain.DTOs;
    public class MissionDto
    {
        public int Id { get; protected set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Tag { get; set; }

        public string Status { get; set; } = string.Empty;

        public int[]? DependencyIds { get; set; }

        public TimeSpan TimeEstimation { get; set; }
}
