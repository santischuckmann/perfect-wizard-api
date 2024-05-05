using MediatR;

namespace perfect_wizard.Application.Queries
{
    public class GetTenantQuery: IRequest<DTOs.TenantDto>
    {
        public string TenantId { get; set; }
    }
}
