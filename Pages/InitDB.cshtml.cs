using Microsoft.AspNetCore.Mvc.RazorPages;
using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;

namespace RMeets.Pages;

public class InitDB : PageModel
{
    public ApplicationContext Context { get; set; }
    public InitDB(ApplicationContext context)
    {
        Context = context;
    }
    public void OnGet()
    {
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
        Context.Targets.AddRange(new List<Target>()
        {
            new Target(){Name = "Найти папика"},
            new Target(){Name = "Купить слона"},
            new Target(){Name = "Рабство"},
        });
        Context.Roles.AddRange(new List<Role>()
        {
            new Role(){Name = "user",Description = "role"},
            new Role(){Name = "moder",Description = "role"},
            new Role(){Name = "admin",Description = "role"},
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
    }
}
