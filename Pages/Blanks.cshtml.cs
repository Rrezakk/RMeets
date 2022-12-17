using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

namespace RMeets.Pages;

public class Blanks : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public BlankDataService _blankDataService;
    private AccountService _accountService;
    public Blanks(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext, BlankDataService blankDataService, AccountService accountService)
    {
        HttpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        _blankDataService = blankDataService;
        _accountService = accountService;
    }
    [BindProperty]
    public int ProfileId { get; set; }
    public IActionResult OnGet(int profileId)
    {
        this.ProfileId = profileId;
        return Page();
    }
    public IActionResult OnGetChooseMainBlank(int profileId,int blankId)
    {
        var profile = _accountService.GetProfileById(profileId);
        if (profile != null)
        {
            ApplicationContext.Attach<Models.Profile>(profile);
            profile.ChosenBlankId = blankId;
            ApplicationContext.Update(profile);
            ApplicationContext.SaveChanges();
        }

        return RedirectToPage("Blanks", new
        {
            profileId
        });
    }

    public IActionResult OnGetDeleteBlank(int profileId,int blankId)
    {
        var profile = _accountService.GetProfileById(profileId);
        Debug.WriteLine($"---------------------------Profile: {profile.Id} {profile.ChosenBlankId} {blankId}");
        if (profile != null)
        {
            ApplicationContext.Attach(profile);
            if (blankId==profile.ChosenBlankId)
            {
                profile.ChosenBlankId = null;
            }
            ApplicationContext.Update(profile);
            ApplicationContext.SaveChanges();
            var blank = ApplicationContext.Blanks.Find(blankId);
            if (blank!=null)
            {
                ApplicationContext.Remove(blank);
            }

            var photos = ApplicationContext.BlankPhotos.Where(x => x.BlankId == blankId);
            foreach (var p in photos)
            {
                ApplicationContext.Remove(p);
            }

            var reactions = ApplicationContext.Reactions.Where(x => x.To != null && (x.From.Id == blankId || x.To.Id == blankId));
            foreach (var r in reactions)
            {
                ApplicationContext.Remove(r);
            }
            ApplicationContext.SaveChanges();
        }
        Debug.WriteLine($"---------------------------Profile: {profile.Id} {profile.ChosenBlankId} {blankId}");

        return RedirectToPage("Blanks", new
        {
            profileId
        });
    }
}
