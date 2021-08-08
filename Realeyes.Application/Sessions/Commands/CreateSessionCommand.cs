using System;
using MediatR;
using Realeyes.Application.DTOs;

namespace Realeyes.Application.Sessions.Commands
{
    public class CreateSessionCommand :IRequest<SessionDTO>
    {
        public CreateSessionCommand(Guid? surveryId)
        {
            SurveryId = surveryId;
        }

        public Guid? SurveryId { get; }
    }
}
