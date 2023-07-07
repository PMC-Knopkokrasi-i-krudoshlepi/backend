namespace DPOBackend.Models.UserModels;

public class UserRegisttrationModel
{
    public UserRegisttrationModel()
    {
        
    }

    public string Name{ get; set; }
    
    public string Password{ get; set; }
    
    public UserRole Role{ get; set; }

    public UserRegisttrationModel(int id, string name, string password, UserRole role)
    {
        Name = name;
        Password = password;
        Role = role;
    }
}