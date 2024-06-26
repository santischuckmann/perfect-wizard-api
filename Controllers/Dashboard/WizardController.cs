﻿using MediatR;
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
        public async Task<IActionResult> CreateWizard(string tenantId)
        {
            string wizardId = await _mediator.Send(new Application.Commands.CreateWizardCommand() { TenantId = tenantId });

            return Ok(wizardId);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateWizard([FromBody] Application.DTOs.WizardDto wizard)
        {
            await _mediator.Send(new Application.Commands.UpdateWizardCommand() { Wizard = wizard });

            return Ok(new EmptyResult());
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
