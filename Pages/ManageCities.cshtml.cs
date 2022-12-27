using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Pages;

public class ManageCities : PageModel
{
    public ApplicationContext _context;
    public ManageCities(ApplicationContext context)
    {
        _context = context;
    }
    public void OnPostAdd(string fact)
    {
        var potentially = _context.CitySet.FirstOrDefault(x => x.Name == fact);
        if(potentially!=null)
            return;
        _context.CitySet.Add(new City() { Name = fact });
        _context.SaveChanges();
    }
}

