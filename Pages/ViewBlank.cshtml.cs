using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RMeets.Contexts;
using RMeets.Enums;
using RMeets.Models;
using System.Configuration;
using System.Diagnostics;

namespace RMeets.Pages;

public class ViewBlank : PageModel
{
    public readonly IHttpContextAccessor HttpContextAccessor;
    public ApplicationContext ApplicationContext { get; set; }
    public Models.Profile? ViewerProfile { get; set; }
    public ViewBlank(IHttpContextAccessor httpContextAccessor, ApplicationContext applicationContext)
    {
        HttpContextAccessor = httpContextAccessor;
        ApplicationContext = applicationContext;

        var login = HttpContextAccessor?.HttpContext?.Session.GetString("user");
        ViewerProfile = ApplicationContext?.Users.FirstOrDefault(u => u.Login == login)?.Profile;
    }
    [BindProperty]
    public int blankId { get; set; }
    [BindProperty]
    public bool meeting { get; set; }
    [BindProperty]
    public bool moderation { get; set; }

    [BindProperty] public int previousBlankId { get; set; }
    public void OnGet(int blankId,bool meeting,int previousBlankId,bool? moderation)
    {
        
        this.blankId = blankId;
        this.meeting = meeting;
        this.previousBlankId = previousBlankId;
        this.moderation = moderation == true;
    }
    private void React( int fromId, int forId,ReactionTypes type)
    {
        var reaction = ApplicationContext.Reactions.FirstOrDefault(x => x.From.Id == forId && x.To!=null &&x.To.Id==fromId);
        if (reaction != null)//other blank used to react before
        {
            ApplicationContext.Attach(reaction);
            //reaction.To = ApplicationContext.Blanks.FirstOrDefault(x => x.Id == fromId) ?? throw new InvalidOperationException();
            reaction.Timestamp2 = DateTime.Now;
            reaction.ReactionType2 = type;
            ApplicationContext.Entry<Reaction>(reaction).State = EntityState.Modified;
            ApplicationContext.SaveChanges();
        }
        else
        {
            var reactedBefore = ApplicationContext.Reactions.FirstOrDefault(x => x.From.Id == fromId && x.To!=null &&x.To.Id==forId);
            if (reactedBefore!=null)//viewer blank used to react before
            {
                ApplicationContext.Attach(reactedBefore);
                reactedBefore.Timestamp1 = DateTime.Now;
                reactedBefore.ReactionType1 = type;
                Debug.WriteLine($"Reacted before");
                ApplicationContext.Entry<Reaction>(reactedBefore).State = EntityState.Modified;
                ApplicationContext.SaveChanges();
            }
            else//no reaction before
            {
                reaction = new Reaction
                {
                    From = ApplicationContext.Blanks.FirstOrDefault(x => x.Id == fromId) ?? throw new InvalidOperationException(),
                    To = ApplicationContext.Blanks.FirstOrDefault(x => x.Id == forId) ?? throw new InvalidOperationException(),
                    Timestamp1 = DateTime.Now,
                    ReactionType1 = type
                };
                ApplicationContext.Reactions.Add(reaction);
                ApplicationContext.SaveChanges();
            }
        }
    }
    public IActionResult OnPostLike(int fromId,int forId)
    {
        React(fromId,forId,ReactionTypes.Like);
        Debug.WriteLine($"{fromId} {forId}");
        return RedirectToPage("ViewBlank",new
        {
            meeting = true,
            blankId,
            previousBlankId
        });
    }
    public IActionResult OnPostDislike(int fromId,int forId)
    {
        React(fromId,forId,ReactionTypes.Dislike);
        Debug.WriteLine($"{fromId} {forId}");
        return RedirectToPage("ViewBlank",new
        {
            meeting = true,
            blankId,
            previousBlankId
        });
    }
}
