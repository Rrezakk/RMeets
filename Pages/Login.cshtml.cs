using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

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
        if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext?.Session.GetString("user")))
        {
            return RedirectToPage("Profile");
        }
        return Page();
    }
    public IActionResult OnPostPerformLogin(string login,string password)
    {
        var u = ApplicationContext.Users.FirstOrDefault(x => x.Login == login);
        if (u == null)
        {
            return Content("Такого пользователя не существует");
        }
        if (u.PasswordHash != password)
        {
            return Content("Неверный пароль");
        }
        _httpContextAccessor.HttpContext?.Session.SetString("user",u.Login);
        return RedirectToPage("Profile");
    }
}
