using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;
    public class Activity : ModelBase<Activity,int>
    {
        public override int Id { get; protected set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Tag { get; set; }

        public Status Status { get; set; }

        [NotMapped]
        public int[]? DependencyIds { get; set; }

        public TimeSpan TimeEstimation { get; set; }

        public bool IsDeleted { get; set; }
    }
