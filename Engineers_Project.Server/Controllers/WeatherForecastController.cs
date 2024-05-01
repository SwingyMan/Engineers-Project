using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IEmailSender _emailSender;


		public WeatherForecastController(IEmailSender emailSender)
		{
			_emailSender = emailSender;
		}
		[HttpGet("/api/email/send")]
		public IActionResult send(string email,string message,string subject)
		{
			_emailSender.SendEmailAsync(email, subject, message);
			return Ok();
		}
	}
}
