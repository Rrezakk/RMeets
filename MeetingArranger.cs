using RMeets.Contexts;
using RMeets.Models;

namespace RMeets;

public static class MeetingArranger
{
    public static int GetNextBlankId(int currentBlankId,ApplicationContext context)
    {
         return context.Blanks.FirstOrDefault(x => x.Id != currentBlankId).Id;
    }
}
