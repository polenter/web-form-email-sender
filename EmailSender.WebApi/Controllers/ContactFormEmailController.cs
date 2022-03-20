using System.Net.Mail;
using EmailSender.WebApi.Extensions;
using EmailSender.WebApi.Options;
using EmailSender.WebApi.Services;
using EmailSender.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmailSender.WebApi.Controllers
{
    [ApiController]
    [Route("api/contactform")]
    public class ContactFormEmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ControllerBase> _logger;
        private readonly IFormClientRepository _formClientRepository;

        public ContactFormEmailController(IFormClientRepository formClientRepository,
            IEmailSender emailSender, ILogger<ContactFormEmailController> logger)
        {
            _formClientRepository = formClientRepository;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SendContactFormMessageViewModel viewModel)
        {
            if (!HoneyPotValidate(viewModel))
            {
                _logger.LogDebug(FormatLogMessage(viewModel, "Honey pot!"));
                return await FakeOk();
            }

            var clientMetadata = await _formClientRepository.GetClientAsync(viewModel.ClientId);
            if (clientMetadata == null)
            {
                _logger.LogDebug(FormatLogMessage(viewModel, "Client not found."));
                return KeyNotFound();
            }

            try
            {
                var subject = string.Format(Captions.EmailSubject, clientMetadata.Website);
                await _emailSender.SendEmailAsync(viewModel.SenderEmail, clientMetadata.TargetEmail, subject,
                    viewModel.Text);

                _logger.LogInformation(FormatLogMessage(viewModel, "Message sent."));
                return Ok();
            }
            catch (SmtpException ex)
            {
                var errorMessage = ex.ConcatInnerMessages();
                _logger.LogError(errorMessage);
                return BadRequest(errorMessage);
            }

        }

        private string FormatLogMessage(SendContactFormMessageViewModel viewModel, string? message = "")
        {
            var model = JsonConvert.SerializeObject(viewModel);
            return $"{message} {model}.".Trim();
        }

        private bool HoneyPotValidate(SendContactFormMessageViewModel viewModel)
        {
            return string.IsNullOrEmpty(viewModel.Subject);
        }

        private IActionResult KeyNotFound()
        {
            ModelState.AddModelError(nameof(SendContactFormMessageViewModel.ClientId), "Unknown client.");
            return BadRequest();
        }

        private async Task<IActionResult> FakeOk()
        {
            await Task.Delay(1700);
            return Ok();
        }
    }
}