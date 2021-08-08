using System;

namespace Realeyes.Domain.Models
{
    public abstract class ModelBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
