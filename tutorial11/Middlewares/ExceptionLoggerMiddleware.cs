namespace tutorial11.Middlewares;

public class ExceptionLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await LogExtensionAsync(context, e);
        }
    }

    private async Task LogExtensionAsync(HttpContext context, Exception e)
    {
        Console.WriteLine(e.ToString());
        await _next(context);
    }
}