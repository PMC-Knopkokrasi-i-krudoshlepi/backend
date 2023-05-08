using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models.UserModels;

public class UserModel
{
    [BsonId]
    public int Id{ get; set; }
    
    [BsonElement("Name")]
    public string Name{ get; set; }
    
    [BsonElement("Password")]
    public string Password{ get; set; }
    
    [BsonElement("Role")]
    public UserRole Role{ get; set; }
}