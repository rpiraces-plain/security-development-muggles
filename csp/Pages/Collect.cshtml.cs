using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Demos.Pages
{
	[IgnoreAntiforgeryToken(Order = 1001)]
	public class CollectModel : PageModel
	{
		private readonly ILogger<CollectModel> _logger;

		public CollectModel(ILogger<CollectModel> logger)
		{
			_logger = logger;
		}

		public async Task<IActionResult> OnPost()
		{
			string requestBody;
			using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
				requestBody = await reader.ReadToEndAsync();
			}

			// Log the request body
			_logger.LogInformation($"Request Body: {requestBody}");

			return StatusCode(200);

		}

		private IActionResult Ok()
		{
			throw new NotImplementedException();
		}
	}
}
