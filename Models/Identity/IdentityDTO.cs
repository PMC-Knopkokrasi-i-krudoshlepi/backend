namespace DPOBackend.Models.Identity;

public class IdentityDTO
{
    public IdentityDTO()
    {
        
    }
    public IdentityDTO(string name, string description)
    {
        Name = name;
        Description = description;
    }
    public IdentityDTO(IdentityType identityType)
    {
        Name = identityType.Name;
        Description = identityType.Description;
    }

    public string Name { get; set; }
    public string Description{ get; set; }
}