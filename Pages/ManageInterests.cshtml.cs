using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Pages;

public class ManageInterests : PageModel
{
    public ApplicationContext _context;
    public ManageInterests(ApplicationContext context)
    {
        _context = context;
    }
    public void OnPostAdd(string fact)
    {
        var potentially = _context.Interests.FirstOrDefault(x => x.Name == fact);
        if(potentially!=null)
            return;
        _context.Interests.Add(new Interest() { Name = fact });
        _context.SaveChanges();
    }
}


