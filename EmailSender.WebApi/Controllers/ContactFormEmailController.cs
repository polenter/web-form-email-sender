using System.Linq;
using System.Threading.Tasks;
using EmailSender.WebApi.Services;
using EmailSender.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactFormEmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IFormClientRepository _formClientRepository;

        public ContactFormEmailController(IFormClientRepository formClientRepository,
            IEmailSender emailSender)
        {
            _formClientRepository = formClientRepository;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SendContactFormMessageViewModel viewModel)
        {
            if (!HoneyPotValidate(viewModel))
            {
                await Task.Delay(1700);
                return Ok();
            }

            var clientMetadata = await _formClientRepository.GetClientAsync(viewModel.ClientId);
            if (clientMetadata == null) return KeyNotFound();

            var subject = string.Format(Captions.EmailSubject, clientMetadata.Website);
            await _emailSender.SendEmailAsync(viewModel.SenderEmail, clientMetadata.TargetEmail, subject,
                viewModel.Text);

            return Ok();
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
    }
}