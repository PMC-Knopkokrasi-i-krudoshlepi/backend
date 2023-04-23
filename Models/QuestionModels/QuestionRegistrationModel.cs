namespace DPOBackend.Models;

public class QuestionRegistrationModel
{
    public QuestionType Type { get; set; }
    
    public string[] PossibleAnswers{ get; set; }
    
    public string[] RightAnswers{ get; set; }
    
    public bool HaveImage{ get; set; }
}