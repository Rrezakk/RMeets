namespace RMeets;

public static class SessionService
{
    public static void SetLogin(IHttpContextAccessor accessor, string login)
    {
        accessor.HttpContext?.Session.SetString("user",login);
    }
    public static string GetLogin(IHttpContextAccessor accessor)
    {
        return accessor.HttpContext?.Session.GetString("user") ??"";
    }
}
