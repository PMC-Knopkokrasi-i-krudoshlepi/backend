using System.Security.Cryptography;
using DPOBackend.Helpers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models.UserModels;

[PrimaryKey("Id")]
public class UserModel
{
    public UserModel()
    {
        
    }
    public int Id{ get; set; }

    public string Code { get; set; }

    public string Name{ get; set; }
    
    public string Password{ get; set; }
    
    public UserRole Role{ get; set; }

    public UserModel(int id, string name, string password, UserRole role)
    {
        Id = id;
        Name = name;
        Password = password;
        Role = role;
        Code = SercurityAlgs.ComputeUserCode(id, name, password);
    }

    public UserModel(int id, UserRegisttrationModel urm)
    {
        Id = id;
        Name = urm.Name;
        Password = urm.Password;
        Role = urm.Role;
        Code = SercurityAlgs.ComputeUserCode(id, urm.Name, urm.Password);
    }
}