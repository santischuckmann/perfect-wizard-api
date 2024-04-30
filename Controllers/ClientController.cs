using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace perfect_wizard.Controllers
{
    [Route("api/client")]
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
        //[HttpPost("{wizardId}")]
        //public async Task<IActionResult> GetWizard(string wizardId)
        //{
        //    var result = await _mediator.Send(new Application.Queries.Client.GetWizardQuery() { WizardId = wizardId });

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<IActionResult> SendWizard([FromBody] Application.DTOs.ResponseDto responseDto)
        {
           await _mediator.Send(new Application.Commands.Client.SendWizardCommand() { Response = responseDto });

            return Ok(new EmptyResult());
        }
    }
}
