namespace perfect_wizard.Application.DTOs
{
    public class ResponseDto
    {
        public string WizardId { get; set; }
        /// <summary>
        /// one or more values of fields that identify the person that finished the wizard 
        /// </summary>
        public string[] Identifier { get; set; }
        public List<ResponseFieldDto> ResponseFields { get; set; }
    }
    public class ResponseFieldDto
    {
        public string FieldId { get; set; }
        public string[] Values { get; set; }
    }
}
