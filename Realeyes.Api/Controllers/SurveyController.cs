using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realeyes.Application.DTOs;
using Realeyes.Application.Surveys.Queries;

namespace Realeyes.Api.Controllers
{
    [ApiController]
    [Route("api/surveys")]
    public class SurveyController: ControllerBase
    {
        private readonly IMediator mediator;

        public SurveyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyDTO>>> GetAll()
        {
            IEnumerable<SurveyDTO> result = await mediator.Send(new GetAllSurveyQuery());
            return Ok(result);
        }
    }
}
