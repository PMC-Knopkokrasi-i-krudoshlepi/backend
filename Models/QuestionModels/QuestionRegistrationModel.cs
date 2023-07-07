namespace DPOBackend.Models;

public class QuestionRegistrationModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public QuestionType Type { get; set; }
    
    public string[] PossibleAnswers{ get; set; }
    
    public string[] RightAnswers{ get; set; }
    
    public ContentType ContentType { get; set; }
}