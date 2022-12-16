using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RMeets.Contexts;
using RMeets.Models;
using System.Diagnostics;

namespace RMeets.Pages;

public class CreateBlank : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public BlankDataService BlankDataService;
    public int? ProfileId { get; set; }
    public CreateBlank(IHttpContextAccessor httpContextAccessor,
        ApplicationContext applicationContext, BlankDataService blankDataService)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        BlankDataService = blankDataService;
        var login =  _httpContextAccessor.HttpContext?.Session.GetString("user");
        var userId = ApplicationContext.Users.FirstOrDefault(x => x.Login == login)?.Id;
        ProfileId = ApplicationContext.Profiles.FirstOrDefault(x => x.UserRef == userId)?.Id;
        ApplicationContext.SaveChanges();
    }
    public IActionResult OnPostCreateById(int id,int genderId,int[] interestIds,
        int[] factIds,string about,int targetId)
    {
        var gender = ApplicationContext.Genders.FirstOrDefault(u => u.Id == genderId);
        var target = ApplicationContext.Targets.FirstOrDefault(u => u.Id == targetId);
        var profile = ApplicationContext.Profiles.FirstOrDefault(u => u.Id == id);
        var interests = 
            ApplicationContext.Interests.Where(x => interestIds.Any(u => x.Id == u)).ToList();
        var facts = 
            ApplicationContext.Facts.Where(x => factIds.Any(u => x.Id == u)).ToList();
        var blank = new Blank()
        {
            Profile = profile ,
            Description = about,
            CurrentGender = gender,
            Target = target,
            Facts = facts,
            Interests = interests,
        };
        var entry = BlankDataService.CreateBlank(blank);
        var blankId = entry.Id;
        
        if (profile?.ChosenBlankId == null)
        {
            profile.ChosenBlankId = blankId;
        }
        ApplicationContext.Update(profile);
        ApplicationContext.SaveChanges();
        Debug.WriteLine(string.Join(' ',interests));
        Debug.WriteLine(id);
        Debug.WriteLine("BlankId = " +blankId);
        return RedirectToPage("EditPhotos",
            new { blankId = blankId.ToString()});
    }
}
