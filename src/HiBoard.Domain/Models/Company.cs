using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HiBoard.Domain.Models;

public class Company : ModelBase<Company, int>
{
    public override int Id { get; protected set; }

    public string? Name { get; set; }

    public ICollection<User>? Users { get; set; }

    public string Admin { get; set; } = string.Empty;

    [NotMapped] //TODO NEED TO MAP THIS
    public ICollection<string>? Departments { get; set; }


    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}