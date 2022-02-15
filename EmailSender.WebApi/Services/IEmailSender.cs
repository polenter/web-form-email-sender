namespace EmailSender.WebApi.Services
{
    public interface IEmailSender
    {
        public System.Threading.Tasks.Task SendEmailAsync(string from, string to, string subject, string htmlMessage);
    }
}