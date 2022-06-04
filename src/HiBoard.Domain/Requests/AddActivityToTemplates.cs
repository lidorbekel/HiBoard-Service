using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.DTOs;
using HiBoard.Domain.Models;

namespace HiBoard.Domain.Requests
{
    public class AddActivityToTemplates
    {
        public ActivityDto? Activity { get; set; }

        public ICollection<int>? TemplatesIds { get; set; }
    }
}
