using DPOBackend.Db;
using DPOBackend.Models.Course;
using DPOBackend.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public class CourseService
{
    public async Task<CourseDTO[]> GetAllAsync()
    {
        CourseDTO[] result;
        using (var ctx = new TestDbContext())
        {
            result = await ctx.Courses
                .Select(course => new CourseDTO(course))
                .ToArrayAsync();
        }

        return result;
    }
    
    public async Task<int> CreateAsync(CourseDTO newCourseDto){
        int uid;
        using (var ctx = new TestDbContext())
        {
            uid = await ctx.Courses.CountAsync() + 1;
            var newCourse = new Course(uid, newCourseDto);
            ctx.Courses.Add(newCourse);
            await ctx.SaveChangesAsync();
        }

        return uid;
    }
}