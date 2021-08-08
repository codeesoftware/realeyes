using System;
using Realeyes.Domain.Enums;

namespace Realeyes.Domain.Models
{
    public class Session:ModelBase
    {
        public Guid  SurveyId { get; set; }
        public Survey  Survey { get; set; }
        public States States { get; set; }
    }
}
