using Infrastructure;
using Infrastructure.ContentSafety;
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
		private readonly TextContentSafetyService _textContentSafety;

		public WeatherForecastController(IEmailSender emailSender,TextContentSafetyService textContentSafetyService)
		{
			_emailSender = emailSender;
			_textContentSafety = textContentSafetyService;
		}
		[HttpGet("/api/email/send")]
		public IActionResult send(string email,string message,string subject)
		{
			_emailSender.SendEmailAsync(email, subject, message);
			return Ok();
		}

		[HttpGet("/api/text/test")]
		public IActionResult test(string text)
		{
			var test = _textContentSafety.AnalyzeText(text);
			return Ok(test);
		}
	}
}
