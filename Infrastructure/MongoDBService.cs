using MongoDB.Driver;

namespace perfect_wizard.Infraestructure
{
    public class MongoDBService
    {
        public IMongoDatabase Database { get; }
        public MongoDBService(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetConnectionString("database_server"));
            var database = client.GetDatabase(configuration.GetConnectionString("database_name"));

            Database = database;
        }
    }
}
