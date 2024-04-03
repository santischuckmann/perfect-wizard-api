using MediatR;

namespace perfect_wizard.Application.Commands
{
    public class SendWizardCommand: IRequest
    {
        public DTOs.ResponseDto ResponseDto { get; set; }
    }
}
