using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DPOBackend.Models;

public class QuestionToFront
{
    public QuestionToFront(Question question)
    {
        Name = question.Name;
        Description = question.Description;
        Type = question.Type;
        PossibleAnswers = question.PossibleAnswers;
    }
    
    public string Name{ get; set; }
    
    public string Description{ get; set; }
    
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public QuestionType Type { get; set; }
    
    public string[] PossibleAnswers{ get; set; }
    
    public string ImageId{ get; set; }
}