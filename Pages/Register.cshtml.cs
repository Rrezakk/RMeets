using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Repositories;

namespace RMeets.Pages;

public class Register : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    private AccountService _accountService;
    public UserRepository _userRepository;
    public Register(IHttpContextAccessor httpContextAccessor, AccountService accountService, UserRepository userRepository, ApplicationContext applicationContext)
    {
        HttpContextAccessor = httpContextAccessor;
        _accountService = accountService;
        _userRepository = userRepository;
        ApplicationContext = applicationContext;
    }
    public ApplicationContext ApplicationContext { get; set; }
    public IActionResult OnPostPerformRegistration(string login,
        string password,string repeatPassword)
    {
        if (password != repeatPassword||string.IsNullOrEmpty(login)||string.IsNullOrEmpty(password))
        {
            return Content("Invalid data");
        }
        if (!_accountService.CreateUser(login, repeatPassword))
        {
            return Content("user not added: maybe exists");
        }
        SessionService.SetLogin(HttpContextAccessor,login);
        return RedirectToPage("CreateProfile");
    }
}
