using MediatR;

namespace perfect_wizard.Application.Queries
{
    public class GetWizardsQuery: IRequest<List<Application.DTOs.MinifiedWizardDto>>
    {
        public string TenantId { get; set; }
    }
}
