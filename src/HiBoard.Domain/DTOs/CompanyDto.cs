using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiBoard.Domain.Models;

namespace HiBoard.Domain.DTOs
{
    public  class CompanyDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public ICollection<string>? Departments { get; set; } = new List<string>();

        public string Description { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}
