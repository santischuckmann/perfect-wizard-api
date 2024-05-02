namespace perfect_wizard.Application.DTOs
{
    public class ResponseDto
    {
        public string WizardId { get; set; }
        public List<ResponseFieldDto> ResponseFields { get; set; }
    }
    public class ResponseFieldDto
    {
        public string FieldId { get; set; }
        public string[] Values { get; set; }
    }
}
