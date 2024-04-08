using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace perfect_wizard.Controllers.Dashboard
{
    [Route("api/tenant")]
    public class TenantController: BaseController
    {
        private readonly IMediator _mediator;
        public TenantController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> CreateTenant([FromBody] Application.DTOs.WizardDto wizard)
        {
            string wizardId = await _mediator.Send(new Application.Commands.CreateWizardCommand() { Wizard = wizard });

            return Ok(wizardId);
        }
    }
}
