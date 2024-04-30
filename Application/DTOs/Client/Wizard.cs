namespace perfect_wizard.Application.DTOs.Client
{
    public class GetWizardAsClientDto
    {
        public string WizardId { get; set; }
        public List<ResponseFieldDto> ResponseFields { get; set; } = new List<ResponseFieldDto>();
        public int ScreenIndex { get; set; }
    }
}
