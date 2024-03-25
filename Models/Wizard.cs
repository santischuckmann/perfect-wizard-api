using MongoDB.Bson.Serialization.Attributes;

namespace perfect_wizard.Models
{
    public class Wizard
    {
        [BsonId]
        public string WizardId { get; set; }
        public string color { get; set; }
        public string title { get; set; }
        public string tenantId { get; set; }
        public List<Screen> screens { get; set; }
    }
    public class Screen
    {
        public List<Field> fields { get; set; }
        public string stepName { get; set; }
    }
    public class Field
    {
        public string fieldType { get; set; }
        public string label { get; set; }
        public string placeholder { get; set; }
        public List<Option> options { get; set; }
        public string name { get; set; }
        public Description? description { get; set; }
    }

    public static class FieldType
    {
        public const string Text = "TEXT";
        public const string Options = "OPTIONS";
        public const string Radio = "RADIO";
        public const string Multiple = "MULTIPLE";
    }

    public class Description
    {
        public string text { get; set; }
        public string position { get; set; }
    }
    public static class Position
    {
        public const string Above = "ABOVE";
        public const string Below = "BELOW";
    }
    public class Option
    {
        [BsonId]
        public string id { get; set; }
        public string description { get; set; }
    }
}
