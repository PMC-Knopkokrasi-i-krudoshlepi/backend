﻿using MongoDB.Bson;
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

    [BsonIgnore]
    public int Count => QuestionsList.Count;

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

    

    public void UpdateImageIds(ObjectId[] objectIds)
    {
        int i = 0;
        foreach (var question in QuestionsList.Where(q => q.ContentType == ContentType.Image))
        {
            if(i >= objectIds.Length)
                break;//TODO: exception хочеца
            question.ContentId = objectIds[i++];
        }
    }
}