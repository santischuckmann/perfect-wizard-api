using MediatR;
using Microsoft.AspNetCore.Mvc;
using perfect_wizard.Application;

namespace perfect_wizard.Controllers.Dashboard
{
    [Route("api/wizard")]
    public class WizardController : BaseController
    {
        private readonly IMediator _mediator;
        public WizardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWizard([FromBody] Application.DTOs.WizardDto wizard)
        {
            string wizardId = await _mediator.Send(new Application.Commands.CreateWizardCommand() { Wizard = wizard });

            return Ok(wizardId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWizards(string tenantId)
        {
            var result = await _mediator.Send(new Application.Queries.GetWizardsQuery() { TenantId = tenantId });

            return Ok(result);
        }

        [HttpGet("{wizardId}")]
        public async Task<IActionResult> GetWizard(string wizardId)
        {
            var result = await _mediator.Send(new Application.Queries.GetWizardQuery() { WizardId = wizardId });

            return Ok(result);
        }
    }
}
