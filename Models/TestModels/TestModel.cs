using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models;

public class TestModel
{
    public TestModel(TestRegistrationModel test)
    {
        Id = new Random(DateTime.Now.Millisecond).Next();//TODO: переделать
        Name = test.Name;
        Description = test.Description;
        LinkedCoursesIds = test.LinkedCoursesIds;
        QuestionsList = test.QuestionsList
            .Select(q => new Question(
                q.Type,
                q.PossibleAnswers,
                q.RightAnswers,
                ObjectId.Empty
            ))
            .ToList();
    }

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

    public void UpdateImageIds(ObjectId[] objectIds)
    {
        int i = 0;
        foreach (var question in QuestionsList)
        {
            question.ImageId = objectIds[i++];
        }
    }
}