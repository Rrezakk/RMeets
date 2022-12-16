using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class ProfileRepository
{
    private ApplicationContext _context;

    public ProfileRepository(ApplicationContext context)
    {
        _context = context;
    }

    public List<Profile> GetAll() => _context.Profiles.ToList();
    public Profile? GetByID(int id) => _context.Profiles.FirstOrDefault(p => p.Id == id);
    public Profile? GetByUserId(int userId) => _context.Profiles.FirstOrDefault(p => p.UserRef == userId);
}