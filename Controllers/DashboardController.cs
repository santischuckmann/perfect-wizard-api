using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using perfect_wizard.Infraestructure;

namespace perfect_wizard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController: ControllerBase
    {
        private readonly MongoDBService _dbService;
        public DashboardController(Infraestructure.MongoDBService dbService) 
        {
            _dbService = dbService;
        }

        protected string UserId => User.Claims?.FirstOrDefault(x => x.Type.Equals("userId", StringComparison.OrdinalIgnoreCase))?.Value;

        [HttpPost]
        public async Task<IActionResult> CreateWizard([FromBody] Models.Wizard wizard)
        {
            var collection = _dbService.Database.GetCollection<Models.Wizard>("wizard");

            string wizardId = Guid.NewGuid().ToString();
            wizard.WizardId = wizardId;

            await collection.InsertOneAsync(wizard);

            return Ok(wizardId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWizards(string tenantId)
        {
            var collection = _dbService.Database.GetCollection<Models.Wizard>("wizard");

            List<Models.Wizard> wizards = await collection.Find(x => x.tenantId == tenantId).ToListAsync();

            return Ok(wizards);
        }
    }
}
