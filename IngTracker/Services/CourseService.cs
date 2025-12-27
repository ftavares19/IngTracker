using Domain;
using IDataAccess;
using IServices;

namespace Services;

public class CourseService : ICourseService
{
    public readonly ICourseRepository CourseRepository;
    
    public CourseService(ICourseRepository courseRepository)
    {
        CourseRepository = courseRepository;
    }
    
    public Course AddCourse(Course course)
    {
        return CourseRepository.Add(course);
    }
    
    public IEnumerable<Course> GetAllCourses()
    {
        return CourseRepository.GetAll();
    }
    
    public Course GetCourseById(int id)
    {
        return CourseRepository.Get(id);
    }
    
    public void DeleteCourse(int id)
    {
        CourseRepository.Delete(id);
    }
    
    public void ModifyCourse(Course courseModified)
    {
        CourseRepository.Modify(courseModified);
    }
    
    public void AddPrerequisite(int courseId, int prerequisiteId)
    {
        var course = CourseRepository.Get(courseId);
        var prerequisite = CourseRepository.Get(prerequisiteId);
        
        if (course == null || prerequisite == null)
        {
            throw new Exception("Course or prerequisite not found");
        }
        
        if (course.Prerequisites == null)
        {
            course.Prerequisites = new List<Course>();
        }
        
        if (!course.Prerequisites.Any(p => p.Id == prerequisiteId))
        {
            course.Prerequisites.Add(prerequisite);
            CourseRepository.Modify(course);
        }
    }
    
    public void RemovePrerequisite(int courseId, int prerequisiteId)
    {
        var course = CourseRepository.Get(courseId);
        
        if (course == null)
        {
            throw new Exception("Course not found");
        }
        
        if (course.Prerequisites != null)
        {
            var prerequisite = course.Prerequisites.FirstOrDefault(p => p.Id == prerequisiteId);
            if (prerequisite != null)
            {
                course.Prerequisites.Remove(prerequisite);
                CourseRepository.Modify(course);
            }
        }
    }
    
    public IEnumerable<Course> GetPrerequisites(int courseId)
    {
        var course = CourseRepository.Get(courseId);
        
        if (course == null)
        {
            throw new Exception("Course not found");
        }
        
        return course.Prerequisites ?? new List<Course>();
    }
}