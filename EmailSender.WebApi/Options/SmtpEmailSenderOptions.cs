namespace EmailSender.WebApi.Options
{
    public class SmtpEmailSenderOptions
    {
        public SmtpEmailSenderOptions()
        {
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}