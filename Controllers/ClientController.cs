using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace perfect_wizard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController: ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{wizardId}")]
        public async Task<IActionResult> GetWizard(string wizardId)
        {
            var result = await _mediator.Send(new Application.Queries.GetWizardQuery() { WizardId = wizardId });

            return Ok(result);
        }

        [HttpPost]
        public IActionResult SendWizard([FromBody] Application.DTOs.ResponseDto responseDto)
        {
            var result = _mediator.Send(new Application.Commands.SendWizardCommand() { Response = responseDto });

            return Ok(result);
        }
    }
}
