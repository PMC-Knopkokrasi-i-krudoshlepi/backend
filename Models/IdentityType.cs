using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models;

public class IdentityType
{
    [BsonElement("Name")]
    public string Name{ get; set; }
    
    [BsonElement("Description")]
    public string Description{ get; set; }
}