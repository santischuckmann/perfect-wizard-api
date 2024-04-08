using MongoDB.Bson.Serialization.Attributes;

namespace perfect_wizard.Models
{
    public class Tenant
    {
        [BsonId]
        public string TenantId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId { get; set; }
    }
}
