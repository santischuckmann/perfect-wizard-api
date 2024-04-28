using MediatR;

namespace perfect_wizard.Application.Queries
{
    public class GetTenantsQuery: IRequest<List<DTOs.TenantDto>>
    {
        public string UserId { get; set; }
    }
}
