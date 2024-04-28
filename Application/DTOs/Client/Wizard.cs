namespace perfect_wizard.Application.DTOs.Client
{
    public class GetWizardAsClientDto
    {
        public string WizardId { get; set; }
        public List<ResponseField> ResponseFields { get; set; } = new List<ResponseField>();
        public int ScreenIndex { get; set; }
    }
}
