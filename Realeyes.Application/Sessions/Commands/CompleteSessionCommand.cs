using System;
using MediatR;
using Realeyes.Application.DTOs;

namespace Realeyes.Application.Sessions.Commands
{
    public class CompleteSessionCommand : IRequest<SessionDTO>
    {
        public CompleteSessionCommand(Guid sessionId)
        {
            SessionId = sessionId;
        }
        public Guid SessionId { get; }
    }
}
