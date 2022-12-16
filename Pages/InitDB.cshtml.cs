using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Pages;

public class InitDB : PageModel
{
    public ApplicationContext Context { get; set; }
    public InitDB(ApplicationContext context)
    {
        Context = context;
    }
    public IActionResult OnGet()
    {
        if (Context.Facts.ToList().Count>0)
        {
            return RedirectToPage("Index");
        }
        Context.Facts.AddRange(new List<Fact>()
        {
            new Fact(){Name = "Крутой"},
            new Fact(){Name = "Интересный"},
            new Fact(){Name = "Мажорик"},
        });
        Context.Interests.AddRange(new List<Interest>()
        {
            new Interest(){Name = "котики"},
            new Interest(){Name = "ухаживание"},
            new Interest(){Name = "цветочки"},
        });
        Context.Users.Add(new User(){Login = "moderator",PasswordHash = "moderator"});
        
        Context.Targets.AddRange(new List<Target>()
        {
            new Target(){Name = "Найти папика"},
            new Target(){Name = "Купить слона"},
            new Target(){Name = "Рабство"},
        });
        Context.CitySet.AddRange(new List<City>()
        {
            new City(){Name = "Ростов-на-Дону"},
            new City(){Name = "Санкт-Петербург"},
            new City(){Name = "Москва"},
        });
        Context.Genders.AddRange(new List<Gender>()
        {
            new Gender(){Name = "мужчина"},
            new Gender(){Name = "женщина"},
            new Gender(){Name = "другое"},
        });
        Context.SaveChanges();
        return RedirectToPage("Index");
    }
}
