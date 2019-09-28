using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Accio.BLL.Models;

namespace Accio.BLL.Services
{
    public class BackgroundEmailSender : IBackgroundEmailSender
    {
        private readonly EmailSettings _settings;
        private readonly IPromotionsService _service;
        private readonly IEmailService _emailService;
        private readonly string _bodyTemplatePath  = Directory.GetCurrentDirectory() + "\\body-template.html";

        public BackgroundEmailSender(EmailSettings settings, IPromotionsService service, IEmailService emailService)
        {
            _emailService = emailService;
            _service = service;
            _settings = settings;

        }

        public async Task DoWork()
        {
            var promotions = await _service.GetPromotions();
            var soonToStart = promotions.Where(p => (DateTime.Now - p.StartDate).Days <= 2);

            var emailBody = new EmailBody {
                Subject = "Soon to start promotions",
                Text = File.ReadAllText(_bodyTemplatePath),
                IsHtml = true
            };

            if (soonToStart.Any())
            {
                await _emailService.SendEmailAsync("n.campusanov@gmail.com", emailBody);
            }
        }
    }
}