using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;

namespace RMeets.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationContext _context;

    public IndexModel(ILogger<IndexModel> logger,ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        
    }
}
