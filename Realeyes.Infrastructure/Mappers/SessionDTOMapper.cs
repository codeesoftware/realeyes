using Boxed.Mapping;
using Realeyes.Application.DTOs;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Mappers
{
    public class SessionDTOMapper : IMapper<Session, SessionDTO>
    {
       
        public void Map(Session source, SessionDTO destination)
        {
            destination.CreatedOn = source.CreatedOn;
            destination.Id = source.Id;
            destination.States = source.States;
            destination.SurveyId = source.SurveyId;
        }
    }
}
