using System.Collections.Generic;

namespace Realeyes.Application.DTOs
{
    public class SurveyDTO: DTOBase
    {
        public SurveyDTO()
        {
            Sessions = new List<SessionDTO>();
        }
        public virtual ICollection<SessionDTO> Sessions { get; set; }
    }
}
