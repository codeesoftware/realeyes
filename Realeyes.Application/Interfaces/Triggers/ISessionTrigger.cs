using System.Threading.Tasks;
using Realeyes.Domain.Models;

namespace Realeyes.Application.Interfaces.Triggers
{
    public interface ISessionTrigger
    {
        public Task<bool> CompletedAsync(Session session);
    }
}
