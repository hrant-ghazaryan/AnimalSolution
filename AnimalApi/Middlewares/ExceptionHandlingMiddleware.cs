using AnimalExceptions;

namespace AnimalApi.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AppException appException)
        {
            LogToFile(appException);
            await WriteErrorResponseAsync(context, appException);
        }
        catch (Exception exception)
        {
            LogToFile(exception);
            await WriteErrorResponseAsync(context, exception);
        }
    }
    private void LogToFile(Exception exception)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(@"D:\Desktop\ExceptionLogs.txt", true))
            {
                writer.WriteLine($"Date: {DateTime.Now} | Message: {exception.Message}");
                writer.WriteLine("StackTrace:");
                writer.WriteLine(exception.StackTrace); 
                writer.WriteLine("-------------------");
            }
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine($"Log file not found: {exception.Message}");
        }
        catch (IOException)
        {
            Console.WriteLine($"I/O error while writing to log file: {exception.Message}");
        }
        catch (Exception)
        {
            Console.WriteLine($"{DateTime.Now}: {exception.Message}");
        }
    }
    private async Task WriteErrorResponseAsync(HttpContext context, Exception catchedException)
    {
        int statusCode = catchedException is AppException appException 
            ? appException.StatusCode 
            : StatusCodes.Status500InternalServerError;

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(new ErrorModel
        {
            ErrorMessage = catchedException.Message,
            ErrorStatus = statusCode
        });
    }
}
