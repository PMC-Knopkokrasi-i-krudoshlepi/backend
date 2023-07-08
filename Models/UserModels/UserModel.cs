using System.Security.Cryptography;
using DPOBackend.Helpers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models.UserModels;

[PrimaryKey("Id")]
public class UserModel
{
    public int Id{ get; set; }
    public string Code { get; set; }
    public string Name{ get; set; }
    public string Surname{ get; set; }
    public string Password{ get; set; }
    public UserRole Role{ get; set; }

    public UserModel(int id, string name, string password, UserRole role, string surname)
    {
        Id = id;
        Name = name;
        Password = password;
        Role = role;
        Surname = surname;
        Code = SercurityAlgs.ComputeUserCode(id, name, password);
    }

    public UserModel(int id, UserRegisttrationModel urm)
    {
        Id = id;
        Surname = urm.Surname;
        Name = urm.Name;
        Password = urm.Password;
        Role = urm.Role;
        Code = SercurityAlgs.ComputeUserCode(id, urm.Name, urm.Password);
    }
}