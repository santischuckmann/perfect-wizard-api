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

        //[HttpPost]
        //public async Task<IActionResult> SendWizard([FromBody] Application.DTOs.)
        //{
        //    var result = await _mediator.Send(new Application.Queries.GetWizardQuery() { WizardId = wizardId });

        //    return Ok(result);
        //}
    }
}
