using System;
using System.Threading;
using System.Threading.Tasks;
using Boxed.Mapping;
using MediatR;
using Microsoft.Extensions.Logging;
using Realeyes.Application.DTOs;
using Realeyes.Application.Exceptions;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Domain.Enums;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Sessions.Commands.Handlers
{
    public class StartSessionCommandHandler : IRequestHandler<StartSessionCommand, SessionDTO>
    {
        private readonly IMapper<Session, SessionDTO> mapper;
        private readonly IRepository<Session> sessionRepository;
        private readonly ILogger<StartSessionCommandHandler> logger;

        public StartSessionCommandHandler(IMapper<Session, SessionDTO> mapper, IRepository<Session> sessionRepository, ILogger<StartSessionCommandHandler> logger)
        {
            this.mapper = mapper;
            this.sessionRepository = sessionRepository;
            this.logger = logger;
        }
        public async Task<SessionDTO> Handle(StartSessionCommand request, CancellationToken cancellationToken)
        {
            if (request.SessionId == default)
            {
                throw new ArgumentException("There is no sessionId!");
            }
            Session session = await sessionRepository.GetByIdAsync(request.SessionId);

            if(session == null)
            {
                throw new SessionException($"Cannot found session!",id: request.SessionId);
            }
            if (session.States != States.New)
            {
                throw new SessionException($"Session state is not valid!", session.States, request.SessionId);
            }
            session.States = States.InProgress;
            session.UpdatedOn = DateTime.Now;
            sessionRepository.Update(session);
            await sessionRepository.SaveAsync();
            logger.LogInformation("Session has just been started!", session);
            var result = this.mapper.Map(session);
            return result;
        }
    }
}
