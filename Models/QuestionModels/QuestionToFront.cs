using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DPOBackend.Models;

public class QuestionToFront
{
    public QuestionToFront(Question question)
    {
        Type = question.Type;
        PossibleAnswers = question.PossibleAnswers;
    }
    
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public QuestionType Type { get; set; }
    
    public string[] PossibleAnswers{ get; set; }
    
    public string ImageId{ get; set; }
}