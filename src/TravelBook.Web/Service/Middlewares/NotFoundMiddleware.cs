namespace TravelBook.Web.Service.Middlewares;

public class NotFoundMiddleware
{
    private readonly RequestDelegate next;

    public NotFoundMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await next.Invoke(context);

        if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
        {
            var r = context.GetEndpoint();
            //Re-execute the request so the user gets the error page
            string originalPath = context.Request.Path.Value;
            context.Items["originalPath"] = originalPath;
            context.Request.Path = "/error/404";
            await next.Invoke(context);
        }
    }
}
