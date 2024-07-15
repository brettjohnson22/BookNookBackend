namespace FullStackAuth_WebAPI.Extensions
{
    public class ResponseCapturingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseCapturingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Create a response stream wrapper
            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            // Invoke the next middleware to generate the response
            await _next(context);

            // Read the response body
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();

            // Log or inspect the responseBody content
            Console.WriteLine(responseBody);

            // Reset the response body stream position
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            // Copy the response body to the original stream and restore it
            await responseBodyStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }
}
