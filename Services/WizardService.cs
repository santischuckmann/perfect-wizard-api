using MongoDB.Driver;

namespace perfect_wizard.Services
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

        public async Task<List<Models.Wizard>> GetWizards(string tenantId)
        {
            List<Models.Wizard> wizards = await _dbService.Wizard.Find(x => x.tenantId == tenantId).ToListAsync();

            return wizards;
        }
        public async Task<Models.Wizard> GetWizard(string wizardId)
        {
            Models.Wizard wizard = await _dbService.Wizard.Find(x => x.WizardId == wizardId).FirstOrDefaultAsync();

            return wizard;
        }
    }
}
