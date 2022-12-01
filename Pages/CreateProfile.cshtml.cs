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
    public IActionResult OnPostPerformCreation(string name,int age,int city,int gender)
    {
        var sU = _httpContextAccessor.HttpContext?.Session.GetString("user");
        var user = ApplicationContext.Users.FirstOrDefault(u => u.Login == sU);
        var gen = ApplicationContext.Genders.FirstOrDefault(g => g.Id == gender);
        var cit = ApplicationContext.Cities.FirstOrDefault(c => c.Id == city);
        var role = ApplicationContext.Roles.FirstOrDefault(r => r.Name == "user");
        var profile = new Models.Profile(userRef:user.Id,name,gen,age,cit,DateTime.Now,role );

        var p = ApplicationContext.Profiles.FirstOrDefault(p => p.User == user);
        if (p != null)
            return Content("Profile already exists");
        
        ApplicationContext.Profiles.Add(profile);
        ApplicationContext.SaveChanges();
        //user.Profile
        //var gender = Request.Form["gender"];
        //var city = Request.Form["city"];
        Debug.WriteLine($"Name: {name} Age: {age} Gender: {gender} City: {city}");
        //if all valid
        return RedirectToPage("Blanks");
        //return RedirectToPage($"EditPhotos/{profile.Id}");
    }
}
