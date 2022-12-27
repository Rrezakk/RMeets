using RMeets.Contexts;

namespace RMeets;

public static class MeetingArranger
{
    public static (int, bool) GetNextBlankId(int currentBlankId, int currentProfileId, int previousBlankId,
        ApplicationContext context)
    {
        if (currentBlankId == -1)
        {
            return (-1,false);
        }
        var reactedIds = context.Reactions.Where(x => (x.From.Id == currentBlankId && x.To != null))
            .Select(x => x.To!.Id).ToList();
        var r2Ids = context.Reactions.Where(x => x.To != null && x.To.Id == currentBlankId && x.ReactionType2 != null)
            .Select(x => x.From.Id).ToList();
        reactedIds.AddRange(r2Ids);
        var queue = context.Blanks.Where(x => x.Profile != null
                                              && x.Id != currentBlankId
                                              && x.Profile.Id != currentProfileId
                                              && reactedIds.All(z => z != x.Id))
            .OrderBy(x => x.Id).Select(x => x.Id).AsEnumerable();
        if (previousBlankId != -1)
        {
            queue = queue.SkipWhile(x => x != previousBlankId);
        }
        int? ans = queue.FirstOrDefault(x => x != previousBlankId, -1);
        if (ans != -1)
            return ((int)ans, true);
        return previousBlankId == -1
            ? (0, false)
            :MeetingArranger.GetNextBlankId(currentBlankId, currentProfileId, -1, context);
    }
}
