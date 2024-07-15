namespace FullStackAuth_WebAPI.Extensions
{
    public class RequestInterceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Place your breakpoint or custom logic here
            // You can inspect the incoming request and perform any necessary operations

            // Call the next middleware in the pipeline

            context.Request.EnableBuffering();
            using var requestBodyStream = new MemoryStream();
            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            // Read the request body content as a string
            using var requestBodyReader = new StreamReader(requestBodyStream);
            var requestBody = await requestBodyReader.ReadToEndAsync();

            // Now you can inspect the requestBody content

            // Reset the request body stream position for subsequent processing
            context.Request.Body.Seek(0, SeekOrigin.Begin);

            await next(context);
        }
    }
}
