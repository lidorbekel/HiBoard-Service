using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiBoard.Domain.Models;

public class Template : ModelBase<Template,int>
{
    public override int Id { get; protected set; }

    public override DateTime CreatedAt { get; protected set; }
        
    public override DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Activity> Activities { get; set; } = new List<Activity>();

    public int CompanyId { get; set; }

    [ForeignKey(nameof(CompanyId))]
    public Company? Company { get; set; }

    public string Department { get; set; } = string.Empty;
        
    public bool IsDeleted { get; set; }
        
}