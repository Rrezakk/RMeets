using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class InterestRepository
{
    private ApplicationContext _context;

    public InterestRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<Interest> GetAll() => _context.Interests.ToList();
}