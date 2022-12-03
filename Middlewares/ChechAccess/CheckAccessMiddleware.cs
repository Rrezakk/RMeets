namespace RMeets.Middlewares.ChechAccess;

public class CheckAccessMiddleware  
{
    private readonly RequestDelegate _next;
 
    public CheckAccessMiddleware(RequestDelegate next)
    {
        this._next = next;
    }
 
    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Session.GetString("user");
        var c = context.Request.Path.ToString().Contains("login") || context.Request.Path.ToString().Contains("reg")|| true;
        if (string.IsNullOrEmpty(token)&&!c)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("session is invalid");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
