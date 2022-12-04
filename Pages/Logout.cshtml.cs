using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RMeets.Pages;

public class Logout : PageModel
{
    private HttpContext _httpContext { get; set; }
    public Logout(IHttpContextAccessor accessor)
    {
        _httpContext = accessor.HttpContext;
    }
    public IActionResult OnGet()
    {
        _httpContext.Session.Remove("user");
        return RedirectToPage("Login");
    }
}
