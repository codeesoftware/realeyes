using System;
using System.Collections.Generic;
using System.Text;
using Realeyes.Domain.Enums;

namespace Realeyes.Application.DTOs
{
    public class SessionDTO : DTOBase
    {
        public Guid SurveyId { get; set; }
        public States States { get; set; }
    }
}
