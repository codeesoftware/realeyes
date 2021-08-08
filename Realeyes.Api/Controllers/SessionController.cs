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
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}/start")]
        public async Task<ActionResult<SessionDTO>> Start([FromBody] Guid surveyId)
        {
            SessionDTO result = await mediator.Send(new StartSessionCommand(surveyId));
            return Ok(result);
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult<SessionDTO>> Complete([FromBody] Guid surveyId)
        {
            SessionDTO result = await mediator.Send(new CompleteSessionCommand(surveyId));
            return Ok(result);
        }
    }
}
