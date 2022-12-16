using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RMeets.Contexts;
using RMeets.Models;
using System.Diagnostics;

namespace RMeets.Pages;

public class EditBlank : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public BlankDataService _blankDataService;
    public int? ProfileId { get; set; }
    [BindProperty]
    public int blankId { get; set; }
    public EditBlank(IHttpContextAccessor httpContextAccessor,
        ApplicationContext applicationContext, BlankDataService blankDataService)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        _blankDataService = blankDataService;
        var login =  _httpContextAccessor.HttpContext?.Session.GetString("user");
        var userId = ApplicationContext.Users.FirstOrDefault(x => x.Login == login)?.Id;
        ProfileId = ApplicationContext.Profiles.FirstOrDefault(x => x.UserRef == userId)?.Id;
        ApplicationContext.SaveChanges();
    }
    public void OnGet(int? blankId)
    {
        if(blankId!=null)
            this.blankId = (int)blankId;
        Debug.WriteLine($"Blank id on get: {blankId}");
    }
    public IActionResult OnPostEdit(int id,int genderId,int[] interestIds,
        int[] factIds,string about,int targetId)
    {
        var interests = ApplicationContext.Interests.Where(x => interestIds.Any(u => x.Id == u)).ToList();
        var facts = ApplicationContext.Facts.Where(x => factIds.Any(u => x.Id == u)).ToList();
        var blank = ApplicationContext.Blanks.FirstOrDefault(x => x.Id == blankId);
        if (blank == null) return Content("blank is null");
        ApplicationContext.Attach(blank);
        blank.Description = about;
        blank.CurrentGender = ApplicationContext.Genders.FirstOrDefault(g => g.Id == genderId);
        blank.Target = ApplicationContext.Targets.FirstOrDefault(g => g.Id == targetId);
        blank.Facts.Clear();
        foreach (var fact in facts)
        {
            blank.Facts.Add(fact);
        }
        blank.Interests.Clear();
        foreach (var interest in interests)
        {
            blank.Interests.Add(interest);
        }
        ApplicationContext.Blanks.Entry(blank).State = EntityState.Modified;
        ApplicationContext.SaveChanges();
        return RedirectToPage("EditBlank", new
        {
            blankId,
            ProfileId = id
        });
    }
    public IActionResult OnPostEditPhotos()
    {
        return RedirectToPage($"EditPhotos", new
        {
            blankId
        });
    }
}
