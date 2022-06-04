using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HiBoard.Application.CustomExceptions.TemplateExceptions
{
    public class ActivityAlreadyExistsInTemplateException : Exception
    {
        [PublicAPI]
        public int TemplateId { get; }
        
        [PublicAPI]
        public int ActivityId { get; }

        public ActivityAlreadyExistsInTemplateException(int activityEntityId, int templateId) : base(
            $"Template with id {templateId} has already activity with id {activityEntityId}")
        {
            TemplateId = templateId;
            ActivityId = activityEntityId;
        }
    }
}