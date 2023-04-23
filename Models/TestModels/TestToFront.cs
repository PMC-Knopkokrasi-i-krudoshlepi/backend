namespace DPOBackend.Models;

public class TestToFront
{
    public TestToFront() {}

    public TestToFront(TestModel test)
    {
        Id = test.Id;
        Name = test.Name;
        Description = test.Description;
        LinkedCoursesIds = test.LinkedCoursesIds;
        QuestionsList = test.QuestionsList.Select(q => new QuestionToFront(q)).ToList();
    }
    
    public int Id{ get; set; }
    
    public string Name{ get; set; }
    
    public string Description{ get; set; }
    
    public List<int> LinkedCoursesIds{ get; set; }
    
    public List<QuestionToFront> QuestionsList{ get; set; }
}