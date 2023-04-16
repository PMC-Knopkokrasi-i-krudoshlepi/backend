using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models;

public class TestModel
{
    [BsonId]
    public int Id{ get; set; }
    
    [BsonElement("Name")]
    public string Name{ get; set; }
    
    [BsonElement("Description")]
    public string Description{ get; set; }
    
    [BsonElement("LinkedCoursesIds")]
    public List<int> LinkedCoursesIds{ get; set; }
    
    [BsonElement("QuestionsList")]
    public List<Question> QuestionsList{ get; set; }

}

/*public class PersonalityTestModel
{
    public int id;
    public string name;
    public string description;
    
    public List<Question> questionsList;

}*/

