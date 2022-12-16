using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class GenderRepository
{
    private readonly ApplicationContext _context;

    public GenderRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<Gender> GetAll() => _context.Genders.ToList();
}