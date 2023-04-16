namespace DPOBackend.Models;

public abstract class Question
{
    public int id;
    public QuestionType type;
}

public class SingleQuestion : Question
{
    public QuestionType type = QuestionType.SINGLE;
    public List<string> possibleAnswers;
    public string anser;
    public string rightAnswer;
}

public class ManyQuestions : Question
{
    public QuestionType type = QuestionType.MANY;
    public List<string> possibleAnswers;
    public List<string> answer;
    public List<string> rightAnswer;
}

public class FreeQuestions : Question
{
    public QuestionType type = QuestionType.FREE;
    public string answer;
    public string rightAnswer;
}