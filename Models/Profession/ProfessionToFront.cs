namespace DPOBackend.Models.Profession;

public class ProfessionToFront
{
    public ProfessionToFront(global::Profession p)
    {
        Name = p.Name;
        Description = p.Description;
        SallaryFrom = p.SallaryFrom;
        SallaryTo = p.SallaryTo;
        Skills = p.Skills;
        Tasks = p.Tasks;
    }

    public string Name{ get; set; }
    
    public string Description{ get; set; }
    
    public int SallaryFrom{ get; set; }
    
    public int SallaryTo{ get; set; }
    
    public string[] Skills{ get; set; }
    
    public string Tasks{ get; set; }
}