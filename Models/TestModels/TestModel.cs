using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DPOBackend.Models;

public class TestModel
{
    public TestModel()
    {
        
    }
    public TestModel([FromServices] TestService service,TestRegistrationModel test)
    {
        Id = service.GetLenth().Result;
        Name = test.Name;
        Description = test.Description;
        LinkedCoursesIds = test.LinkedCoursesIds;
        QuestionsList = test.QuestionsList
            .Select(q => 
                new Question(
                    q.Type,
                    q.PossibleAnswers,
                    q.RightAnswers,
                    q.ContentType,
                    ObjectId.Empty
                )
            )
            .ToList();
    }

    public int Id{ get; set; }
    
    public string Name{ get; set; }
    
    public string Description{ get; set; }
    
    public List<int> LinkedCoursesIds{ get; set; }
    
    public List<Question> QuestionsList{ get; set; }

    public async Task<int> GetRightAnswerCount(string[][] answer) =>
        await Task.Run(() => 
        {
            if (answer.Length != QuestionsList.Count)
                return 0;
            int c = 0;
            for (int i = 0; i < answer.Length; i++)
            {
                if (Enumerable.SequenceEqual(answer[i], QuestionsList[i].RightAnswers))
                    c++;
            }
            return c;
        });

    

    /*public void UpdateImageIds(ObjectId[] objectIds)
    {
        int i = 0;
        foreach (var question in QuestionsList.Where(q => q.ContentType == ContentType.Image))
        {
            if(i >= objectIds.Length)
                break;//TODO: exception хочеца
            question.ContentId = objectIds[i++];
        }
    }*/
}