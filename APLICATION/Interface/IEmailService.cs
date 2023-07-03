using System.Threading.Tasks;

namespace APLICATION.Interface
{
	public interface IEmailService
	{
		Task<bool> SendEmailAsync(string to, string subject, string htmlContent);
	}
}
