using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

namespace RMeets.Pages;

public class CreateBlank : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }

    public CreateBlank(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    public void OnGet()
    {
        
    }
    public IActionResult OnPostCreateBlank()
    {
        //target.TimesUsed++
        return RedirectToPage("EditPhotos");
    }
}
