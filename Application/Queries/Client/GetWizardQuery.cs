using MediatR;
using perfect_wizard.Application.DTOs.Client;

namespace perfect_wizard.Application.Queries.Client
{
    public class GetWizardQuery: IRequest<DTOs.WizardDto>
    {
        public GetWizardAsClientDto Body { get; set; }
    }
}
