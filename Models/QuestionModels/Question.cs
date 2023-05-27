﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DPOBackend.Models;

[PrimaryKey("Id")]
public class Question
{
    public Question()
    {
        
    }
    public Question(QuestionType argType, string[] argPossibleAnswers, string[] argRightAnswers, ContentType contentType, int objectId)
    {
        Type = argType;
        PossibleAnswers = argPossibleAnswers;
        RightAnswers = argRightAnswers;
        ContentType = contentType;
        ContentId = objectId;
    }
    
    public int Id { get; set; }

    [BsonElement("Type")]
    [BsonRepresentation(BsonType.String)]
    [JsonConverter(typeof(StringEnumConverter))]
    public QuestionType Type { get; set; }
    
    [BsonElement("PossibleAnswers")]
    public string[] PossibleAnswers{ get; set; }
    
    [BsonElement("RightAnswers")]
    public string[] RightAnswers{ get; set; }
    
    [BsonElement("ContentType")]
    public ContentType ContentType { get; set; }

    [BsonElement("ContentId")]
    public int ContentId{ get; set; }
}