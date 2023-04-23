using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models;

public class TestRegistrationModel
{
    public string Name{ get; set; }
    
    public string Description{ get; set; }
    
    public List<int> LinkedCoursesIds{ get; set; }
    
    public List<QuestionRegistrationModel> QuestionsList{ get; set; }
}