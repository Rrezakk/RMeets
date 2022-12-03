namespace RMeets.Middlewares.ChechAccess;

public static class CheckAccessMidlewareExtensions
{
    public static IApplicationBuilder UseAccessUrl(this IApplicationBuilder app)  
    {  
        return app.UseMiddleware<CheckAccessMiddleware>();  
    } 
}