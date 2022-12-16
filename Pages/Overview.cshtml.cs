using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Repositories;

namespace RMeets.Pages;

public class Overview : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public Models.Profile? ViewerProfile { get; set; }
    private BlankDataService _blankDataService;
    public ReactionsService ReactionsService;
    private UserRepository _userRepository;
    public Overview(ApplicationContext applicationContext,IHttpContextAccessor httpContextAccessor, BlankDataService blankDataService, ReactionsService reactionsService, UserRepository userRepository)
    {
        ApplicationContext = applicationContext;
        this.HttpContextAccessor = httpContextAccessor;
        _blankDataService = blankDataService;
        ReactionsService = reactionsService;
        _userRepository = userRepository;

        var login = SessionService.GetLogin(httpContextAccessor);
        ViewerProfile = _userRepository?.GetByLogin(login)?.Profile;
    }
}
