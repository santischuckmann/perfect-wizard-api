using MediatR;
using MongoDB.Driver;
using perfect_wizard.Application.Queries;
using perfect_wizard.Application.DTOs;

namespace perfect_wizard.Application.Handlers.Queries
{
    public class GetWizardsQueryHandler : IRequestHandler<GetWizardsQuery, List<MinifiedWizardDto>>
    {
        private readonly Infrastructure.MongoDBService _dbService;
        public GetWizardsQueryHandler(Infrastructure.MongoDBService mongoDBService)
        {
            _dbService = mongoDBService;
        }
        public async Task<List<MinifiedWizardDto>> Handle(GetWizardsQuery request, CancellationToken cancellationToken)
        {
            List<Models.Wizard> wizards = await _dbService.Wizard.Find(x => x.tenantId == request.TenantId).ToListAsync();

            List<MinifiedWizardDto> wizardDtos = new List<MinifiedWizardDto>();

            foreach (var wizard in wizards)
            {
                var wizardDto = new MinifiedWizardDto()
                {
                    TenantId = wizard.tenantId,
                    Title = wizard.title,
                    WizardId = wizard.WizardId
                };

                List<MinifiedScreenDto> screenDtos = new List<MinifiedScreenDto>();

                foreach (var screen in wizard.screens)
                {
                    screenDtos.Add(new MinifiedScreenDto()
                    {
                        FieldCount = screen.fields.Count,
                        StepName = screen.stepName,
                    });
                }

                wizardDto.Screens = screenDtos;

                wizardDtos.Add(wizardDto);
            }

            return wizardDtos;
        }
    }
}
