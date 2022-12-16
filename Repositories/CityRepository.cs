using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class CityRepository
{
    private readonly ApplicationContext _context;

    public CityRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<City> GetAll() => _context.CitySet.ToList();
}