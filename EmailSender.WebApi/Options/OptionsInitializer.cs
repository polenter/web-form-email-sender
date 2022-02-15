using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmailSender.WebApi.Options
{
    internal class OptionsInitializer: IPostConfigureOptions<SmtpEmailSenderOptions>
    {
        private readonly ILogger<OptionsInitializer> _logger;

        public OptionsInitializer(ILogger<OptionsInitializer> logger)
        {
            _logger = logger;
        }

        public void PostConfigure(string name, SmtpEmailSenderOptions options)
        {
            _logger.LogInformation("SMTP:{user}@{host}:{port}", options.Username, options.Host, options.Port);
            if (string.IsNullOrEmpty(options.Password))
            {
                _logger.LogWarning("SMTP server password is not defined.");
            }
        }
    }
}