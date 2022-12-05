using RMeets.Models;

namespace RMeets.SpecialModels;

public class ShortBlankAdministratorViewModel
{
    public int Id;
    public string Name;
    public bool Moderated;
    public string Target;

    public ShortBlankAdministratorViewModel(int id, string name, string target,bool? prohibited)
    {
        Id = id;
        Name = name;
        Moderated = prohibited!=null;
        Target = target;
    }
}
