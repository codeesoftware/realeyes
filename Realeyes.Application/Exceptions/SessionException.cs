using System;
using Realeyes.Domain.Enums;

namespace Realeyes.Application.Exceptions
{
    public class SessionException:Exception
    {
        public SessionException(string message, States? state= null, Guid? id = null):base(message)
        {
            if (id.HasValue)
            {
                Data.Add(nameof(id), id);
            }
            if (state.HasValue)
            {
                Data.Add(nameof(state), state);
            }
        }    
    }
}
