using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class TargetRepository
{
    private readonly ApplicationContext _context;

    public TargetRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<Target> GetAll() => _context.Targets.ToList();
}