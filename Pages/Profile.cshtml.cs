using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

namespace RMeets.Pages;

public class Profile : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Profile(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    public ApplicationContext ApplicationContext { get; set; }
    public int ProfileId { get; set; }
    public IActionResult OnGet()
    {
        var login = _httpContextAccessor.HttpContext?.Session.GetString("user");
        var user = ApplicationContext.Users.FirstOrDefault(x => x.Login == login)?.Id;
        var p = ApplicationContext.Profiles.FirstOrDefault(x => x.UserRef == user);
        if (p == null)
        {
            return RedirectToPage("CreateProfile");
        }
        ProfileId = p.Id;
        return Page();
    }
}
