using System.Collections.Generic;
using MediatR;
using Realeyes.Application.DTOs;

namespace Realeyes.Application.Surveys.Queries
{
    public class GetAllSurveyQuery : IRequest<IEnumerable<SurveyDTO>>
    {
    }
}
