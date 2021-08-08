using System.Collections.Generic;

namespace Realeyes.Domain.Models
{
    public class Survey : ModelBase
    {
        public Survey()
        {
            Sessions = new List<Session>();
        }
        public virtual ICollection<Session> Sessions { get; private set; }
    }
}
