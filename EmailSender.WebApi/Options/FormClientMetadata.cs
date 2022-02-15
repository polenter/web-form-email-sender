namespace EmailSender.WebApi.Options
{
    public partial class FormClientMetadata
    {
        public FormClientMetadata()
        {
        }

        public string ClientId { get; set; }
        public string TargetEmail { get; set; }
        public string Website { get; set; }
    }
}