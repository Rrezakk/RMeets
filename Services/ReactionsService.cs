using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;
using RMeets.Repositories;

namespace RMeets;

public class ReactionsService
{
    private ApplicationContext _context;
    private ReactionRepository _reactionRepository;
    public ReactionsService(ApplicationContext context, ReactionRepository reactionRepository)
    {
        _context = context;
        _reactionRepository = reactionRepository;
    }
    public List<Reaction> GetReactionsPendingFrom(int myBlankId)
    {
        return _context.Reactions.Where(r => r.From.Id == myBlankId && r.To != null && (r.ReactionType2 == null))
            .ToList();
    }
    public List<Reaction> GetReactionsPendingFromAns(int myBlankId)
    {
        return _context.Reactions.Where(r => r.To != null
                                             && r.To.Id == myBlankId
                                             && r.ReactionType2 != null//ответила моя анкета
                                             && !(r.ReactionType1 == r.ReactionType2 &&
                                                  r.ReactionType1 == ReactionTypes.Like)).ToList();//не мэтч
    }
    public List<Reaction> GetReactionsPendingTo(int myBlankId)
    {
        return _context.Reactions.Where(r => r.To!=null && r.To.Id == myBlankId
                                                        && r.ReactionType1 != r.ReactionType2
                                                        && r.ReactionType2 ==null
                                                        && r.ReactionType1 != ReactionTypes.Dislike).ToList();
    }
    public List<Reaction> GetReactionsMatchFromMe(int myBlankId)
    {
        return _context.Reactions.Where(r => ((r.From.Id == myBlankId))
                                             && r.ReactionType1 == r.ReactionType2
                                             && r.ReactionType1 == ReactionTypes.Like).ToList();
    }
    public List<Reaction> GetReactionsMatchToMe(int myBlankId)
    {
        return _context.Reactions.Where(r => (((r.To != null&& r.To.Id == myBlankId)))
                                             && r.ReactionType1 == r.ReactionType2
                                             && r.ReactionType1 == ReactionTypes.Like).ToList();
    }
}
