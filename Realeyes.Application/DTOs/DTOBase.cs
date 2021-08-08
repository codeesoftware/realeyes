using System;

namespace Realeyes.Application.DTOs
{
    public abstract class DTOBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
