using System;
using System.Threading;
using System.Threading.Tasks;
using Boxed.Mapping;
using MediatR;
using Realeyes.Application.DTOs;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Sessions.Commands.Handlers
{
    class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDTO>
    {
        private readonly IMapper<Session, SessionDTO> mapper;
        private readonly IRepository<Survey> surveryRepository;
        private readonly IRepository<Session> sessionRepository;

        public CreateSessionCommandHandler(IMapper<Session, SessionDTO> mapper, IRepository<Session> sessionRepository, IRepository<Survey> surveryRepository)
        {
            this.mapper = mapper;
            this.surveryRepository = surveryRepository;
            this.sessionRepository = sessionRepository;
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
                //exception
            }
            var sesssion = new Session()
            {
                CreatedOn = now,
                States = Domain.Enums.States.New,
                Survey = survey
            };

            await sessionRepository.InsertAsync(sesssion);
            await sessionRepository.SaveAsync();

            var result = this.mapper.Map(sesssion);
            return result;
        }
    }
}
