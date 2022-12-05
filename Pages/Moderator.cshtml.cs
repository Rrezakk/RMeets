using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;

namespace RMeets.Pages;

public class Moderator : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Moderator(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    public ApplicationContext ApplicationContext { get; set; }
    [BindProperty]
    public int profileId { get; set; }
    public IActionResult OnGet()
    {
        var login = _httpContextAccessor.HttpContext?.Session.GetString("user");
        var u = ApplicationContext.Users.FirstOrDefault(x => x.Login == login);
        if (u?.Role != UserRoles.Moderator)
        {
            return RedirectToPage("Login");
        }
        return Page();
    }
    public IActionResult OnPostModerate(int blankId, bool moderationResult)
    {
        var blank = ApplicationContext.Blanks.FirstOrDefault(x => x.Id == blankId);
        ApplicationContext.Attach<Blank>(blank);
        blank.BlockedByModerator = !moderationResult;
        ApplicationContext.Update(blank);
        ApplicationContext.SaveChanges();
        return RedirectToPage("Moderator");
    }
}
