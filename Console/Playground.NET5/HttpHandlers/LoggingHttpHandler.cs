using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Playground.NET5.HttpHandlers
{
	public class LoggingHttpHandler : DelegatingHandler
	{
		private readonly ILogger<LoggingHttpHandler> _logger;

		public LoggingHttpHandler(ILogger<LoggingHttpHandler> logger)
		{
			_logger = logger;
		}

		public LoggingHttpHandler(ILogger<LoggingHttpHandler> logger, HttpMessageHandler delegatingHandler) : base(delegatingHandler)
		{
			_logger = logger;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			using (var requestScope = _logger.BeginScope(request.RequestUri))
			{
				_logger.LogInformation($"{request.Method.Method}: {request.RequestUri}");

				_logger.LogInformation("Request Headers: ");

				foreach (var httpRequestHeader in request.Headers)
				{
					_logger.LogInformation($"{httpRequestHeader.Key} = {string.Join(", ", httpRequestHeader.Value)}");
				}

				if (request.Content != null)
				{
					_logger.LogInformation("Request Content Headers: ");

					foreach (var httpRequestHeader in request.Content.Headers)
					{
						_logger.LogInformation($"{httpRequestHeader.Key} = {string.Join(", ", httpRequestHeader.Value)}");
					}

					var content = await request.Content.ReadAsStringAsync(cancellationToken);
					_logger.LogInformation($"Request Content: {content}");
				}


				var response = await base.SendAsync(request, cancellationToken);

				_logger.LogInformation("Response");
				_logger.LogInformation($"Is Success: {response.IsSuccessStatusCode}");
				_logger.LogInformation($"Status Code: {response.StatusCode}");
				_logger.LogInformation($"Reason Phrase: {response.ReasonPhrase}");

				_logger.LogInformation("Response Headers: ");

				foreach (var httpResponseHeader in response.Headers)
				{
					_logger.LogInformation($"{httpResponseHeader.Key} = {string.Join(", ", httpResponseHeader.Value)}");
				}

				_logger.LogInformation("Response Trailing Headers: ");

				foreach (var httpResponseHeader in response.TrailingHeaders)
				{
					_logger.LogInformation($"{httpResponseHeader.Key} = {string.Join(", ", httpResponseHeader.Value)}");
				}

				_logger.LogInformation("Response Content Headers: ");

				foreach (var httpRequestHeader in response.Content.Headers)
				{
					_logger.LogInformation($"{httpRequestHeader.Key} = {string.Join(", ", httpRequestHeader.Value)}");
				}

				var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

				_logger.LogInformation($"Content: {responseContent}");

				return response;
			}
		}
	}
}