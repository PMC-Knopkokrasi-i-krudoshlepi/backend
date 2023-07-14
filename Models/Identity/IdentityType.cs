using Microsoft.EntityFrameworkCore;

namespace DPOBackend.Models.Identity;

[PrimaryKey("Id")]
public class IdentityType
{
    public IdentityType()
    {
        
    }
    public IdentityType(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public IdentityType(int id, IdentityDTO newIdent)
    {
        Id = id;
        Name = newIdent.Name;
        Description = newIdent.Description;
    }
    
    public int Id;
    public string Name { get; set; }
    public string Description{ get; set; }
}