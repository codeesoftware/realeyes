using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realeyes.Application.DTOs;
using Realeyes.Application.Sessions.Commands;

namespace Realeyes.Api.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionController
    {
        private readonly IMediator mediator;

        public SessionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<SessionDTO>> Create([FromBody] Guid? surveyId)
        {
            SessionDTO result = await mediator.Send(new CreateSessionCommand(surveyId));
            return (result);
        }
    }
}
