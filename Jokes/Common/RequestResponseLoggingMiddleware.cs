using System;
namespace Jokes.Common
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            using (var responseBodyStream = new MemoryStream())
            {
                var originalResponseBody = response.Body;
                response.Body = responseBodyStream;

                await _next(context);

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseBodyText = new StreamReader(responseBodyStream).ReadToEnd();

                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBody);
                response.Body = originalResponseBody;

                _logger.LogInformation($"Request Method: {request.Method}, Path: {request.Path}, Request Body: {requestBodyText}");
                _logger.LogInformation($"Response Status Code: {response.StatusCode}, Response Body: {responseBodyText}");
            }
        }
    }

}