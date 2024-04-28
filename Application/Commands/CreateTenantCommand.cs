using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class CreateTenantCommand: IRequest<string>
    {
        public DTOs.TenantDto Tenant { get; set; }
        public string UserId { get; set; }
    }
}
