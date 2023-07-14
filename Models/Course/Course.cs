using Microsoft.EntityFrameworkCore;

namespace DPOBackend.Models.Course;

[PrimaryKey("Id")]
public class Course
{
    public Course()
    {
        
    }
    public Course(int id, CourseDTO dto)
    {
        Id = id;
        Name = dto.Name;
        Description = dto.Description;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}