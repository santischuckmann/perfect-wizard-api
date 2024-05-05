using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class CreateWizardCommand: IRequest<string>
    {
        public string TenantId { get; set; }
    }
}
