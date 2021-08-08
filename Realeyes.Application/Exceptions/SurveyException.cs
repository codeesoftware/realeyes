using System;
using Realeyes.Domain.Enums;

namespace Realeyes.Application.Exceptions
{
    public class SurveyException : Exception
    {
        public SurveyException(string message, Guid? id = null):base(message)
        {
            if (id.HasValue)
            {
                Data.Add(nameof(id), id);
            }
        }    
    }
}
