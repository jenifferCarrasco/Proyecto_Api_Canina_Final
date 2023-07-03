using APLICATION.Interface;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PERSISTENCE.Canina.Services
{
	internal class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;
		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<bool> SendEmailAsync(string to, string subject, string htmlContent)
		{
			string apikey = _configuration.GetValue<string>("SendGridKey");
			var sendGridClient = new SendGridClient(apikey);

			var from = new EmailAddress("sysvacrd.notificaciones@gmail.com", "Notificaciones");
			var toEmailAddress = new EmailAddress(to);
			var plainTextContent = "text/html";
			var msg = MailHelper.CreateSingleEmail(from, toEmailAddress, subject, plainTextContent, htmlContent);

			var respon = await sendGridClient.SendEmailAsync(msg);

			return respon.IsSuccessStatusCode;
		}
	}
}
