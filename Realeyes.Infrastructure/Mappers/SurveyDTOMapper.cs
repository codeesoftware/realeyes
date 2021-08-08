using System.Linq;
using Boxed.Mapping;
using Realeyes.Application.DTOs;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Mappers
{
    public class SurveyDTOMapper : IMapper<Survey, SurveyDTO>
    {
        private readonly IMapper<Session, SessionDTO> sessionMapper;

        public SurveyDTOMapper(IMapper<Session, SessionDTO> sessionMapper)
        {
            this.sessionMapper = sessionMapper;
        }

        public void Map(Survey source, SurveyDTO destination)
        {
            destination.CreatedOn = source.CreatedOn;
            destination.Id = source.Id;
            if (source.Sessions.Any())
            {
                destination.Sessions = sessionMapper.MapList<Session, SessionDTO>(source.Sessions);
            }
        }
    }
}
