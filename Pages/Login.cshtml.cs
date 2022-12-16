using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;
using RMeets.Repositories;

namespace RMeets.Pages;

public class Login : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private AccountService _accountService;
    private UserRepository _userRepository;
    public Login(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext, AccountService accountService, UserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;
        _accountService = accountService;
        _userRepository = userRepository;
    }
    public ApplicationContext ApplicationContext { get; set; }
    public IActionResult OnGet()
    {
        var login = SessionService.GetLogin(_httpContextAccessor);
        var u = _userRepository.GetByLogin(login);
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
        var u = _userRepository.GetByLogin(login);
        if (u == null)
        {
            return Content("Такого пользователя не существует");
        }
        if (!_accountService.ValidateUser(login,password))
        {
            return Content("Неверный пароль");
        }
        SessionService.SetLogin(_httpContextAccessor,login);
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
