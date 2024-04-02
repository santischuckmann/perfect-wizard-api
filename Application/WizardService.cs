using MongoDB.Driver;

namespace perfect_wizard.Application
{
    public class WizardService
    {
        private readonly Infrastructure.MongoDBService _dbService;
        public WizardService(Infrastructure.MongoDBService dbService) 
        { 
            _dbService = dbService;
        }

        public async Task<string> CreateWizard(Models.Wizard wizard)
        {
            string wizardId = Guid.NewGuid().ToString();
            wizard.WizardId = wizardId;

            await _dbService.Wizard.InsertOneAsync(wizard);

            return wizardId;
        }

        public async Task<List<DTOs.MinifiedWizardDto>> GetWizards(string tenantId)
        {
            List<Models.Wizard> wizards = await _dbService.Wizard.Find(x => x.tenantId == tenantId).ToListAsync();

            List<DTOs.MinifiedWizardDto> wizardDtos = new List<DTOs.MinifiedWizardDto>();

            foreach (var wizard in wizards)
            {
                var wizardDto = new DTOs.MinifiedWizardDto()
                {
                    Color = wizard.color,
                    TenantId = wizard.tenantId,
                    Title = wizard.title,
                    WizardId = wizard.WizardId
                };

                List<DTOs.MinifiedScreenDto> screenDtos = new List<DTOs.MinifiedScreenDto>();

                foreach (var screen in wizard.screens)
                {
                    screenDtos.Add(new DTOs.MinifiedScreenDto()
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
        public async Task<DTOs.WizardDto> GetWizard(string wizardId)
        {
            Models.Wizard wizard = await _dbService.Wizard.Find(x => x.WizardId == wizardId).FirstOrDefaultAsync();

            if (wizard is null)
                throw new Exception($"There is no Wizard that matches the id = '{wizardId}'");

            DTOs.WizardDto wizardDto = new DTOs.WizardDto()
            {
                Color = wizard.color, 
                Screens = wizard.screens,
                TenantId = wizard.tenantId,
                Title = wizard.title,
                WizardId = wizard.WizardId
            };

            return wizardDto;
        }
    }
}
