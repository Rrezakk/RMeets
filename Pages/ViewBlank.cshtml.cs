using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Pages;

public class ViewBlank : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public ViewBlank(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        HttpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    [BindProperty]
    public int blankId { get; set; }
    public void OnGet(int blankId)
    {
        this.blankId = blankId;
    }
}
