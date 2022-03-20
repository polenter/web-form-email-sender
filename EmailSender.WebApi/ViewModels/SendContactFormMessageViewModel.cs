using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EmailSender.WebApi.ViewModels
{
    public class SendContactFormMessageViewModel
    {
        public SendContactFormMessageViewModel()
        {
        }

        [Required]
        [StringLength(100)]
        public string ClientId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string SenderEmail { get; set; }

        /// <summary>
        /// Soll nicht ausgefüllt werden, soll schlägt Honey pot zu.
        /// </summary>
        [StringLength(200)]
        public string? Subject { get; set; }

        [StringLength(1000)]
        public string Text { get; set; }

    }
}