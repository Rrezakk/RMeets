using Microsoft.AspNetCore.Hosting.Server.Features;
using System.Diagnostics;

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
        Debug.WriteLine($"Token: {token} Path: {context.Request.Path.ToString()}");
        var path = context.Request.Path.ToString().ToLowerInvariant();
        var c = path.Contains("/login") || path.Contains("/register")|| path.Equals("/");
        if (string.IsNullOrEmpty(token)&&!c)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("authorization required!");
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
