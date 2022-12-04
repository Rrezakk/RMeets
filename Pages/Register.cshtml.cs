using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Pages;

public class Register : PageModel
{
    public readonly IHttpContextAccessor _httpContextAccessor;
    public Register(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
    }
    public ApplicationContext ApplicationContext { get; set; }
    public void OnGet()
    {
        
    }
    public IActionResult OnPostPerformRegistration(string login,
        string password,string repeatPassword)
    {
        if (password != repeatPassword||string.IsNullOrEmpty(login)||string.IsNullOrEmpty(password))
        {
            //ban
            return Content("Invalid data");
        }
        //Debug.WriteLine($"{login} {password} {repeatPassword}");
        var u = ApplicationContext.Users.FirstOrDefault(x => x.Login == login);
        if (u != null)
        {
            return Content("user already exists");
        }
        var user = new User(login,password);
        ApplicationContext.Users.Add(user);
        ApplicationContext.SaveChanges();
        _httpContextAccessor.HttpContext?.Session.SetString("user",user.Login);
        return RedirectToPage("CreateProfile");
    }
}
