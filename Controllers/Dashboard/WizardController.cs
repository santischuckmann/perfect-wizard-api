using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using perfect_wizard.Infrastructure;

namespace perfect_wizard.Controllers.Dashboard
{
    [Route("api/wizard")]
    public class WizardController : BaseController
    {
        private readonly Services.WizardService _wizardService;
        public WizardController(Services.WizardService wizardService)
        {
            _wizardService = wizardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWizard([FromBody] Models.Wizard wizard)
        {
            string wizardId = await _wizardService.CreateWizard(wizard);

            return Ok(wizardId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWizards(string tenantId)
        {
            var wizards = _wizardService.GetWizards(tenantId);

            return Ok(wizards);
        }

        [HttpGet("{wizardId}")]
        public async Task<IActionResult> GetWizard(string wizardId)
        {
            var wizard = _wizardService.GetWizards(wizardId);

            return Ok(wizard);
        }
    }
}
