using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realeyes.Application.DTOs;
using Realeyes.Application.Sessions.Commands;

namespace Realeyes.Api.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly IMediator mediator;

        public SessionController(IMediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpPost]
        public async Task<ActionResult<SessionDTO>> Create([FromBody] Guid? surveyId = null)
        {
            SessionDTO result = await mediator.Send(new CreateSessionCommand(surveyId));
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut("{id}/start")]
        public async Task<ActionResult<SessionDTO>> Start(Guid id)
        {
            SessionDTO result = await mediator.Send(new StartSessionCommand(id));
            return Ok(result);
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult<SessionDTO>> Complete( Guid id)
        {
            SessionDTO result = await mediator.Send(new CompleteSessionCommand(id));
            return Ok(result);
        }
    }
}
