namespace DPOBackend.Models.Course;

public class CourseDTO
{
    public CourseDTO()
    {
        
    }

    public CourseDTO(Course course)
    {
        Name = course.Name;
        Description = course.Description;
        StartDate = course.StartDate;
        EndDate = course.EndDate;
    }
    public CourseDTO(string name, string description, string startDate, string endDate)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}