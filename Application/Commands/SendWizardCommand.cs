using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class SendWizardCommand: IRequest
    {
        public DTOs.ResponseDto Response { get; set; }
    }
}
