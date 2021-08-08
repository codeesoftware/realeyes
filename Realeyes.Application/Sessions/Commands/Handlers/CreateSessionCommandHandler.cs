using System;
using System.Threading;
using System.Threading.Tasks;
using Boxed.Mapping;
using MediatR;
using Microsoft.Extensions.Logging;
using Realeyes.Application.DTOs;
using Realeyes.Application.Exceptions;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Sessions.Commands.Handlers
{
    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDTO>
    {
        private readonly IMapper<Session, SessionDTO> mapper;
        private readonly IRepository<Survey> surveryRepository;
        private readonly ILogger<CreateSessionCommandHandler> logger;
        private readonly IRepository<Session> sessionRepository;

        public CreateSessionCommandHandler(IMapper<Session, SessionDTO> mapper,
            IRepository<Session> sessionRepository,
            IRepository<Survey> surveryRepository,
            ILogger<CreateSessionCommandHandler> logger)
        {
            this.mapper = mapper;
            this.sessionRepository = sessionRepository;
            this.surveryRepository = surveryRepository;
            this.logger = logger;
        }
        public async Task<SessionDTO> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            Survey survey;
            if (!request.SurveryId.HasValue)
            {
                survey = new Survey()
                {
                    CreatedOn = now,                    
                };
            }
            else
            {
                survey = await surveryRepository.GetByIdAsync(request.SurveryId.Value);
            }
            if(survey == null)
            {
                throw new SurveyException($"Cannot found survey!",request.SurveryId.Value);
            }
            var session = new Session()
            {
                CreatedOn = now,
                States = Domain.Enums.States.New,
                Survey = survey
            };

            await sessionRepository.InsertAsync(session);
            await sessionRepository.SaveAsync();
            logger.LogInformation("Session has just been created!", session);
            var result = this.mapper.Map(session);
            return result;
        }
    }
}
