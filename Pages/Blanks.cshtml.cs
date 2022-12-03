using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

namespace RMeets.Pages;

public class Blanks : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public Blanks(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        HttpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    [BindProperty]
    public int ProfileId { get; set; }
    public IActionResult OnGet()
    {
        return Page();
    }
}
