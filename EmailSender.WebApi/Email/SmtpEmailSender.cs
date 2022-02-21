using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using EmailSender.WebApi.Options;
using EmailSender.WebApi.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmailSender.WebApi.Email
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient.send
    /// </summary>
    internal class SmtpEmailSender : IEmailSender
    {
        private readonly IOptionsSnapshot<SmtpEmailSenderOptions> _options;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptionsSnapshot<SmtpEmailSenderOptions> options, ILogger<SmtpEmailSender> logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task SendEmailAsync(string replyTo, string to, string subject, string htmlMessage)
        {
            using (var client = CreateSmtpClientInstance())
            {
                var emailFrom = !string.IsNullOrEmpty(_options.Value.From) ? _options.Value.From : _options.Value.Username;
                var m = new MailMessage(emailFrom, to, subject, htmlMessage)
                {
                    IsBodyHtml = true,
                    ReplyToList = { replyTo }
                };

                _logger.LogInformation("Sending email '{subject}' from '{from}' to '{to}'.", subject, replyTo, to);
                await client.SendMailAsync(m);
            }
        }

        private SmtpClient CreateSmtpClientInstance()
        {
            return new SmtpClient(_options.Value.Host, _options.Value.Port)
            {
                Credentials = GetCredentials(),
                EnableSsl = true
            };
        }

        private ICredentialsByHost GetCredentials()
        {
            return new NetworkCredential(_options.Value.Username, _options.Value.Password);
        }
    }

}