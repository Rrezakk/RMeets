using Microsoft.EntityFrameworkCore;
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
    public Profile Add(Profile p)
    {
        var ans = _context.Profiles.Add(p);
        _context.SaveChanges();
        return ans.Entity;
    }
    public void Edit(Profile profile)
    {
        _context.Profiles.Attach(profile);
        _context.Profiles.Entry(profile).State = EntityState.Modified;
        _context.SaveChanges();
    }
    public Profile? FindById(int id)
    {
        return _context.Profiles.Find(id);
    }
}