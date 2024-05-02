using MongoDB.Bson.Serialization.Attributes;

namespace perfect_wizard.Models
{
    public class Response
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ResponseId { get; set; }
        public string WizardId { get; set; }
        /// <summary>
        /// one or more values of fields that identify the person that finished the wizard 
        /// </summary>
        public string[] identifier { get; set; }
        public List<ResponseField> responseFields { get; set; }
    }
    public class ResponseField
    {
        public string FieldId { get; set; }
        public string[] values { get; set; }
    }
}
