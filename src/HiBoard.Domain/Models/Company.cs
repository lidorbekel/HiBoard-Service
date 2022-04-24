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

    public string Admin { get; set; } = string.Empty;

    [NotMapped]
    public ICollection<string>? Departments { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}