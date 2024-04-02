using perfect_wizard.Models;

namespace perfect_wizard.Application.DTOs
{
    public class WizardDto
    {
        public string WizardId { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
        public string TenantId { get; set; }
        public List<Screen> Screens { get; set; }
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
