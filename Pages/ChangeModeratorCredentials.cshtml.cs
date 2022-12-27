using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;
using RMeets.Repositories;

namespace RMeets.Pages;

public class ChangeModeratorCredentials : PageModel
{
    private AccountService _accountService;
    public UserRepository _userRepository;
    public readonly IHttpContextAccessor _httpContextAccessor;
    private ApplicationContext _context;
    public ChangeModeratorCredentials(AccountService accountService, UserRepository userRepository, IHttpContextAccessor httpContextAccessor, ApplicationContext context)
    {
        _accountService = accountService;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }
    public void OnGet()
    {
        
    }
    public void OnPostPerformEdit(string login,string password)
    {
        var previousLogin = SessionService.GetLogin(_httpContextAccessor);
        var currentUser = _userRepository.GetByLogin(previousLogin);
        _context.Attach<User>(currentUser);
        _context.Users.Update(currentUser);

        var possibleUser = _userRepository.GetByLogin(login);
        if (!string.IsNullOrEmpty(login) && possibleUser==null)
        {
            currentUser.Login = login;
        }
        if (!string.IsNullOrEmpty(password))
        {
            currentUser.PasswordHash = AccountService.GetStringSha256Hash(password);
        }
        _context.SaveChanges();
        
        SessionService.SetLogin(_httpContextAccessor,currentUser.Login);
    }
}
