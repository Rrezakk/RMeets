using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class FactRepository
{
    private readonly ApplicationContext _context;

    public FactRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<Fact> GetAll() => _context.Facts.ToList();
}