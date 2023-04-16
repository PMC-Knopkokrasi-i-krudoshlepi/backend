namespace DPOBackend.Models;

public class CourseTestModel
{
    public int id;
    public string name;
    public string description;
    public List<int> linkedCoursesIds;
    public List<Question> questionsList;

}

public class PersonalityTestModel
{
    public int id;
    public string name;
    public string description;
    
    public List<Question> questionsList;

}

