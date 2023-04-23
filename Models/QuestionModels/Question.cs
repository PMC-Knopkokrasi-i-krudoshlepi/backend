using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DPOBackend.Models;

public class Question
{
    public Question(QuestionType argType, string[] argPossibleAnswers, string[] argRightAnswers, ObjectId objectId)
    {
        Type = argType;
        PossibleAnswers = argPossibleAnswers;
        RightAnswers = RightAnswers;
        ImageId = objectId;
    }

    [BsonElement("Type")]
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public QuestionType Type { get; set; }
    
    [BsonElement("PossibleAnswers")]
    public string[] PossibleAnswers{ get; set; }
    
    [BsonElement("RightAnswers")]
    public string[] RightAnswers{ get; set; }
    
    [BsonElement("ImageId")]
    public ObjectId ImageId{ get; set; }
}