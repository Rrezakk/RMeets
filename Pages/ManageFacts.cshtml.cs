using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Pages;

public class ManageFacts : PageModel
{
    public ApplicationContext _context;
    public ManageFacts(ApplicationContext context)
    {
        _context = context;
    }
    public void OnPostAdd(string fact)
    {
        var potentially = _context.Facts.FirstOrDefault(x => x.Name == fact);
        if(potentially!=null)
            return;
        _context.Facts.Add(new Fact() { Name = fact });
        _context.SaveChanges();
    }
}
