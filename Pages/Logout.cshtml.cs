using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RMeets.Pages;

public class Logout : PageModel
{
    private IHttpContextAccessor _httpContext { get; set; }
    public Logout(IHttpContextAccessor accessor)
    {
        _httpContext = accessor;
    }
    public IActionResult OnGet()
    {
        SessionService.LogOut(_httpContext);
        return RedirectToPage("Index");
    }
}
