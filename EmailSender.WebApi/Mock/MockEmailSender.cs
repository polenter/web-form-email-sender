using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EmailSender.WebApi.Services;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

#if DEBUG
namespace EmailSender.WebApi.Mock
{
    internal class MockEmailSender : IEmailSender
    {
        private readonly ISystemClock _systemClock;

        public MockEmailSender(ISystemClock systemClock)
        {
            _systemClock = systemClock;
        }

        public Task SendEmailAsync(string from, string to, string subject, string htmlMessage)
        {
            var folderName = GenerateFolderName();
            var directory = Path.Combine(@"t:\email-sender.657463", folderName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var obj = new
            {
                From = from,
                To = to,
                Subject = subject,
                HtmlMessage = htmlMessage
            };
            var text = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var messageFileName = Path.Combine(directory, "message.json");
            return File.WriteAllTextAsync(messageFileName, text, Encoding.UTF8);
        }

        private string GenerateFolderName()
        {
            var time = _systemClock.UtcNow.ToLocalTime().ToString("yyyyMMdd-HHmmss");
            return $"{time}-{Guid.NewGuid():N}";
        }
    }

}
#endif