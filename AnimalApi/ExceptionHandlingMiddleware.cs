using AnimalExceptions;

namespace AnimalApi;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AnimalException animalException)
        {
            LogToFile(animalException);

            context.Response.StatusCode = animalException.StatusCode;
            await context.Response.WriteAsJsonAsync(new ErrorModel
            {
                ErrorMessage = animalException.Message,
                ErrorStatus = animalException.StatusCode
            });
        }
        catch (Exception exception)
        {
            LogToFile(exception);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorModel
            {
                ErrorMessage = "An unexpected error occurred.",
                ErrorStatus = StatusCodes.Status500InternalServerError
            });
        }
    }
    private void LogToFile(Exception exception)
    {
        using (StreamWriter writer = new StreamWriter(@"D:\Desktop\ExceptionLogs.txt", true))
        {
            writer.WriteLine($"Date: {DateTime.Now}");
            writer.WriteLine($"Message: {exception.Message}");
            writer.WriteLine("StackTrace:");
            writer.WriteLine(exception.StackTrace);
            writer.WriteLine("-----------------------------------");
        }
    }
}
