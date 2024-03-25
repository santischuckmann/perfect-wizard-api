using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace perfect_wizard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController: ControllerBase
    {
        [HttpGet("{wizardId}")]
        public async Task<IActionResult> GetWizard(string wizardId)
        {
            var client = new MongoClient("mongodb+srv://m001-student:44351950a@sandbox.vlsoedn.mongodb.net/?retryWrites=true&w=majority&appName=Sandbox");
            var database = client.GetDatabase("perfect_wizard");

            var collection = database.GetCollection<Models.Wizard>("wizard");

            Models.Wizard wizard = await collection.Find(x => x.WizardId == wizardId).FirstOrDefaultAsync();

            return Ok(wizard);
        }
    }
}
