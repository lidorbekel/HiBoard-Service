using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.TemplateExceptions
{
    public class TemplateNotFoundException : Exception
    {
        [PublicAPI]
        public int TemplateId { get; }

        public TemplateNotFoundException(int templateId) : base($"Template with id {templateId} not found") =>
            TemplateId = templateId;
    }
}
