using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class BlankRepository
{
    private readonly ApplicationContext _context;
    public BlankRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<Blank> GetAll() => _context.Blanks.ToList();
    public Blank? GetById(int id) => _context.Blanks.FirstOrDefault(b => b.Id == id);
    public List<Blank> GetByProfileId(int profileId) => _context.Blanks.Where(b => b.ProfileId == profileId).ToList();
    
}