using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class UserRepository
{
    private ApplicationContext _context;
    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<User> GetAll() => _context.Users.ToList();
    public User? GetById(int id) => _context.Users.FirstOrDefault(x => x.Id == id);
    public User? GetByLogin(string login) => _context.Users.FirstOrDefault(l => l.Login == login);
    public User AddUser(User u)
    {
        var user = _context.Users.Add(u).Entity;
        _context.SaveChanges();
        return user;
    }
}