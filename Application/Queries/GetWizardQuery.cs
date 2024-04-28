using MediatR;

namespace perfect_wizard.Application.Queries
{
    public class GetWizardQuery: IRequest<DTOs.WizardDto>
    {
        public string WizardId { get; set; }
    }
}
