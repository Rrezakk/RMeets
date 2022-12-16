using RMeets.Contexts;
using RMeets.Models;

namespace RMeets.Repositories;

public class ReactionRepository
{
    private ApplicationContext _context;

    public ReactionRepository(ApplicationContext context)
    {
        _context = context;
    }
    public Reaction? GetBlankReactionById(int blankId) =>
        _context.Reactions.FirstOrDefault(r => r.To != null && (r.From.Id == blankId || r.To.Id == blankId));
}