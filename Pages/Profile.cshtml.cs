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
    public Profile(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext, UserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        _userRepository = userRepository;
    }
    [BindProperty]
    public int profileId { get; set; }
    public IActionResult OnGet()
    {
        var login = _httpContextAccessor.HttpContext?.Session.GetString("user");
        var user = ApplicationContext.Users.FirstOrDefault(x => x.Login == login)?.Id;
        var p = ApplicationContext.Profiles.FirstOrDefault(x => x.UserRef == user);
        if (p == null)
        {
            return RedirectToPage("CreateProfile");
        }
        profileId = p.Id;
        return Page();
    }
    public IActionResult OnPostEditProfile(string name,int age,int city,int gender,string contact,int ProfileId)
    {
        Debug.WriteLine($"Profile id: {ProfileId}");
        var gen = ApplicationContext.Genders.FirstOrDefault(g => g.Id == gender);
        var cit = ApplicationContext.CitySet.FirstOrDefault(c => c.Id == city);
        var profile = ApplicationContext.Profiles.FirstOrDefault(x => x.Id == ProfileId);
        Debug.WriteLine($"Name: {name} Age: {age} Gender: {gender} City: {city}");
        if (profile == null) return Content("profile null");
        profile.Name = name;
        profile.Age = age;
        profile.SocialMediaLink = contact;
        if (cit != null) profile.City = cit;
        if (gen != null) profile.Gender = gen;
        ApplicationContext.Profiles.Attach(profile);
        ApplicationContext.Profiles.Entry(profile).State = EntityState.Modified;
        ApplicationContext.SaveChanges();
        Debug.WriteLine($"Name: {name} Age: {age} Gender: {gender} City: {city}");
        return RedirectToPage("Profile",new
        {
            profileId = profile.Id
        });
    }
}
