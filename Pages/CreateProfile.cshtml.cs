using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using System.Diagnostics;

namespace RMeets.Pages;

public class CreateProfile : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public CreateProfile(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    public void OnGet()
    {
        
    }
    public IActionResult OnPostPerformCreation(string name,int age,int city,int gender,string contact)
    {
        var sU = _httpContextAccessor.HttpContext?.Session.GetString("user");
        var user = ApplicationContext.Users.FirstOrDefault(u => u.Login == sU);
        var gen = ApplicationContext.Genders.FirstOrDefault(g => g.Id == gender);
        var cit = ApplicationContext.CitySet.FirstOrDefault(c => c.Id == city);
        var profile = new Models.Profile()
        {
            UserRef = user.Id,
            Name = name,
            Gender = gen,
            Age = age,
            City = cit,
            RegistrationTimestamp = DateTime.Now,
            SocialMediaLink = contact
        };

        var p = ApplicationContext.Profiles.FirstOrDefault(p => p.User == user);
        if (p != null)
            return Content("Profile already exists");
        var res = ApplicationContext.Profiles.Add(profile);
        ApplicationContext.SaveChanges();
        Debug.WriteLine($"Name: {name} Age: {age} Gender: {gender} City: {city}");
        //if all valid
        return RedirectToPage("Blanks",new{profileId = res.Entity.Id});
    }
}
