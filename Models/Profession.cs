using MongoDB.Bson.Serialization.Attributes;

public class Profession
{
    [BsonElement("Name")]
    public string Name{ get; set; }
    
    [BsonElement("Description")]
    public string Description{ get; set; }
    
    [BsonElement("SallaryFrom")]
    public int SallaryFrom{ get; set; }
    
    [BsonElement("SallaryTo")]
    public int SallaryTo{ get; set; }
    
    [BsonElement("Skills")]
    public string[] Skills{ get; set; }
    
    [BsonElement("Tasks")]
    public string Tasks{ get; set; }
}