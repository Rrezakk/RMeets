using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;

namespace RMeets.Pages;

public class Login : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public Login(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    public ApplicationContext ApplicationContext { get; set; }
    public IActionResult OnGet()
    {
        var login = _httpContextAccessor.HttpContext?.Session.GetString("user");
        var u = ApplicationContext.Users.FirstOrDefault(x => x.Login == login);
        var profile = u?.Profile;
        if (profile!=null)
        {
            return RedirectToPage("Profile",new
            {
                profileId = profile?.Id
            });
        }
        return Page();
    }
    public IActionResult OnPostPerformLogin(string login,string password)
    {
        var u = ApplicationContext.Users.FirstOrDefault(x => x.Login == login);
        //return Content($"User: {u.Id} {u.Login} {u.PasswordHash}");
        if (u == null)
        {
            return Content("Такого пользователя не существует");
        }
        if (u.PasswordHash != password)
        {
            return Content("Неверный пароль");
        }
        _httpContextAccessor.HttpContext?.Session.SetString("user",u.Login);
        
        if (u?.Role == null)
        {
            ApplicationContext.Attach<User>(u);
            u.Role = UserRoles.User;
            ApplicationContext.Update(u);
            ApplicationContext.SaveChanges();
        }
        var role = u?.Role;
        var profileId = ApplicationContext.Profiles.FirstOrDefault(p => p.UserRef == u.Id)?.Id;
        return role switch
        {
            UserRoles.Moderator => RedirectToPage("Moderator", new { profileId }),
            UserRoles.User => RedirectToPage("Profile", new { profileId }),
            _ => RedirectToPage("Profile", new { profileId })
        };
    }
}
