namespace PhoneBox.ExceptionHandling;

public static class ExceptionHandlerMiddleWareExtension
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}