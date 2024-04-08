using MongoDB.Driver;
using perfect_wizard.Models;

namespace perfect_wizard.Infrastructure
{
    public class MongoDBService
    {
        public IMongoCollection<Wizard> Wizard { get; set; }
        public IMongoCollection<Response> Response { get; set; }
        public IMongoCollection<Tenant> Tenant { get; set; }
        public IMongoCollection<User> User { get; set; }
        public MongoDBService(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetConnectionString("database_server"));
            var database = client.GetDatabase(configuration.GetConnectionString("database_name"));

            Wizard = database.GetCollection<Wizard>("wizard");
        }
    }
}
