namespace perfect_wizard.Models
{
    public class Response
    {
        public string WizardId { get; set; }
        public List<ResponseField> responseFields { get; set; }
    }
    public class ResponseField
    {
        public string FieldId { get; set; }
        public string[] values { get; set; }
    }
}
