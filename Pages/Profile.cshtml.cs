using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RMeets.Contexts;
using RMeets.Repositories;
using System.Diagnostics;

namespace RMeets.Pages;

public class Profile : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public ApplicationContext ApplicationContext { get; set; }
    private UserRepository _userRepository;
    public AccountService _accountService;
    public BlankDataService _BlankDataService;
    public Profile(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext, UserRepository userRepository, AccountService accountService, BlankDataService blankDataService)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        _userRepository = userRepository;
        _accountService = accountService;
        _BlankDataService = blankDataService;
    }
    [BindProperty]
    public int profileId { get; set; }
    public IActionResult OnGet()
    {
        var login = SessionService.GetLogin(_httpContextAccessor);
        var user = _userRepository.GetByLogin(login);
        var p = user?.Profile;
        if (p == null)
        {
            return RedirectToPage("CreateProfile");
        }
        profileId = p.Id;
        return Page();
    }
    public IActionResult OnPostEditProfile(string name,int age,int city,int gender,string contact,int ProfileId)
    {
        var gen = ApplicationContext.Genders.FirstOrDefault(g => g.Id == gender);
        var cit = ApplicationContext.CitySet.FirstOrDefault(c => c.Id == city);
        var profile = ApplicationContext.Profiles.FirstOrDefault(x => x.Id == ProfileId);
        if (profile == null) return Content("profile null");
        profile.Name = name;
        profile.Age = age;
        profile.SocialMediaLink = contact;
        if (cit != null) profile.City = cit;
        if (gen != null) profile.Gender = gen;
        _accountService.EditProfile(profile);
        return RedirectToPage("Profile",new
        {
            profileId = profile.Id
        });
    }
}
