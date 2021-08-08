using System;
using MediatR;
using Realeyes.Application.DTOs;

namespace Realeyes.Application.Sessions.Commands
{
    public class StartSessionCommand:IRequest<SessionDTO>
    {
        public StartSessionCommand(Guid sessionId)
        {
            SessionId = sessionId;
        }
        public Guid SessionId { get; }
    }
}
