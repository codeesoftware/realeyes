using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Boxed.Mapping;
using MediatR;
using Realeyes.Application.DTOs;
using Realeyes.Application.Interfaces.Repositories;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Surveys.Queries.Handlers
{
    class GetAllSurveyQueryHandler : IRequestHandler<GetAllSurveyQuery, IEnumerable<SurveyDTO>>
    {
        private readonly IMapper<Survey, SurveyDTO> mapper;
        private readonly IRepository<Survey> surveryRepository;

        public GetAllSurveyQueryHandler(IMapper<Survey, SurveyDTO> mapper, IRepository<Survey> surveryRepository)
        {
            this.mapper = mapper;
            this.surveryRepository = surveryRepository;
        }
        public async Task<IEnumerable<SurveyDTO>> Handle(GetAllSurveyQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Survey> surveys = await surveryRepository.GetAllAsync(m => m.Sessions);
            IEnumerable<SurveyDTO> result = mapper.MapList(surveys);
            return result;
        }
    }
}
