using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.CompanyExceptions;

public class CompanyNotFoundException : Exception
{
    [PublicAPI] 
    public int CompanyId { get; }

    public CompanyNotFoundException(int companyId) : base($"Company with id {companyId} not found") =>
        CompanyId = companyId;
}