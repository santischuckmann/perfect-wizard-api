using MongoDB.Bson.Serialization.Attributes;
using perfect_wizard.Models;

namespace perfect_wizard.Application.DTOs
{
    public class WizardDto
    {
        public string WizardId { get; set; }
        public string Title { get; set; }
        public string TenantId { get; set; }
        public List<ScreenDto> Screens { get; set; }
    }
    public class ScreenDto
    {
        public List<FieldDto> Fields { get; set; }
        public string StepName { get; set; }
    }
    public class FieldDto
    {
        public string? FieldId { get; set; }
        public string Label { get; set; }
        public string Placeholder { get; set; }
        public List<Option> Options { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsIdentifier { get; set; }
        public int MinValuesRequired { get; set; }
        public Description? Description { get; set; }
    }

    public class MinifiedWizardDto
    {
        public string WizardId { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
        public string TenantId { get; set; }
        public List<MinifiedScreenDto> Screens { get; set; }
    }

    public class MinifiedScreenDto
    {
        public string StepName { get; set; }
        public int FieldCount { get; set; }
    }
}
