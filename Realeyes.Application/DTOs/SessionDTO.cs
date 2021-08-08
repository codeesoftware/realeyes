using System;
using Realeyes.Domain.Enums;

namespace Realeyes.Application.DTOs
{
    public class SessionDTO : DTOBase
    {
        public Guid SurveyId { get; set; }
        public States States { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
