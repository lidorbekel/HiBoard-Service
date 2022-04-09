using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.Enums;

namespace HiBoard.Domain.Models;
    public class Mission : ModelBase<Mission,int>
    {
        public override int Id { get; protected set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Tag { get; set; }

        public Status Status { get; set; }

        public int[]? DependencyIds { get; set; }

        public TimeSpan TimeEstimation { get; set; }

        public bool IsDeleted { get; set; }
    }
