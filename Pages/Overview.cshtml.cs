using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

namespace RMeets.Pages;

public class Overview : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public Models.Profile? ViewerProfile { get; set; }
    public Overview(ApplicationContext applicationContext,IHttpContextAccessor HttpContextAccessor)
    {
        ApplicationContext = applicationContext;
        this.HttpContextAccessor = HttpContextAccessor;
        
        var login = HttpContextAccessor?.HttpContext?.Session.GetString("user");
        ViewerProfile = ApplicationContext?.Users.FirstOrDefault(u => u.Login == login)?.Profile;
    }
    public void OnGet()
    {
        
    }
}
