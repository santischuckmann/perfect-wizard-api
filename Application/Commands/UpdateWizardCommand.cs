using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class UpdateWizardCommand: IRequest
    {
        public DTOs.WizardDto Wizard { get; set; }
    }
}
