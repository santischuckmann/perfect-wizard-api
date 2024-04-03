using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class CreateWizardCommand: IRequest<string>
    {
        public DTOs.WizardDto Wizard { get; set; }
    }
}
