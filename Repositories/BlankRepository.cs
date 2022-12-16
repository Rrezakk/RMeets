using Microsoft.EntityFrameworkCore;
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
    public Blank Create(Blank blank)
    {
        var b = _context.Blanks.Add(blank);
        _context.SaveChanges();
        return b.Entity;
    }
    public Blank Edit(Blank blank)
    {
        _context.Attach(blank);
        _context.Blanks.Entry(blank).State = EntityState.Modified;
        _context.SaveChanges();
        return blank;
    }
}