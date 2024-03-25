﻿using MongoDB.Driver;
using perfect_wizard.Models;

namespace perfect_wizard.Infrastructure
{
    public class MongoDBService
    {
        public IMongoCollection<Wizard> Wizard { get; set; }
        public MongoDBService(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetConnectionString("database_server"));
            var database = client.GetDatabase(configuration.GetConnectionString("database_name"));

            Wizard = database.GetCollection<Wizard>("wizard");
        }
    }
}
