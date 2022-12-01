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
    public void OnGet()
    {
        
    }
}
