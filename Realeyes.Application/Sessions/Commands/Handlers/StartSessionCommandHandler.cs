using System;
using System.Threading;
using System.Threading.Tasks;
using Boxed.Mapping;
using MediatR;
using Realeyes.Application.DTOs;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Domain.Enums;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Sessions.Commands.Handlers
{
    class StartSessionCommandHandler : IRequestHandler<StartSessionCommand, SessionDTO>
    {
        private readonly IMapper<Session, SessionDTO> mapper;
        private readonly IRepository<Session> sessionRepository;

        public StartSessionCommandHandler(IMapper<Session, SessionDTO> mapper, IRepository<Session> sessionRepository)
        {
            this.mapper = mapper;
            this.sessionRepository = sessionRepository;
        }
        public async Task<SessionDTO> Handle(StartSessionCommand request, CancellationToken cancellationToken)
        {
            if (request.SessionId == default)
            {
                throw new ArgumentException("There is no sessionId!");
            }
            Session session = await sessionRepository.GetByIdAsync(request.SessionId);

            if (session.States != States.New)
            {
                throw new InvalidOperationException($"Session state is not valid! state: {session.States}");
            }
            session.States = States.InProgress;
            session.UpdatedOn = DateTime.Now;
            sessionRepository.Update(session);
            await sessionRepository.SaveAsync();
            var result = this.mapper.Map(session);
            return result;
        }
    }
}
