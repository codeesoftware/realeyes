using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Realeyes.Api.Controllers
{
    [ApiController]
    [Route("api/systems")]
    public class SystemController : ControllerBase
    {
     

        private readonly ILogger<SystemController> _logger;
        private readonly IMediator mediator;

        public SystemController(ILogger<SystemController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public string HealthCheck()
        {
            return "Ok";
        }
    }
}
