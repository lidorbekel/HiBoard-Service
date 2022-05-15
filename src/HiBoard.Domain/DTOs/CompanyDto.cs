using System.ComponentModel.DataAnnotations.Schema;
using HiBoard.Domain.Models;

namespace HiBoard.Domain.DTOs
{
    public  class CompanyDto
    {
        [NotMapped]
        public int Id { get; protected set; }
        
        public string Name { get; set; } = string.Empty;

        public List<string>? Departments { get; set; } = new();

        public string Description { get; set; } = string.Empty;

        public ICollection<User>? Users { get; set; }
    }
}
