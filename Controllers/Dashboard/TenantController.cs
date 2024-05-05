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
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] Application.DTOs.TenantDto tenant)
        {
            string tenantId = await _mediator.Send(new Application.Commands.CreateTenantCommand() { 
                Tenant = tenant,
                UserId = UserId
            });

            return Ok(tenantId);
        }

        [HttpGet]
        public async Task<IActionResult> GetTenants()
        {
            var tenants = await _mediator.Send(new Application.Queries.GetTenantsQuery() { UserId = UserId });

            return Ok(tenants);
        }

        [HttpGet("{tenantId}")]
        public async Task<IActionResult> GetTenants(string tenantId)
        {
            var tenant = await _mediator.Send(new Application.Queries.GetTenantQuery() { TenantId = tenantId });

            return Ok(tenant);
        }

    }
}
