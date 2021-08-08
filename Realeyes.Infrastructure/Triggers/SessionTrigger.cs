using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Realeyes.Application.Interfaces.Triggers;
using Realeyes.Domain.Models;

namespace Realeyes.Infrastructure.Triggers
{
    class SessionTrigger : ISessionTrigger
    {
        private readonly ILogger<SessionTrigger> logger;

        public SessionTrigger(ILogger<SessionTrigger> logger)
        {
            this.logger = logger;
        }
        public async  Task<bool> CompletedAsync(Session session)
        {
            logger.LogInformation("Data processing is started!",session);
            await Task.Delay(2000);
            logger.LogInformation("Data processing is finished!", session);

            return true;
        }
    }
}
