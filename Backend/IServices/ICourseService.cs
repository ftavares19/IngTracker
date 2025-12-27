using Domain;

namespace IServices;

public interface ICourseService
{
    Course AddCourse(Course course);
    IEnumerable<Course> GetAllCourses();
    Course GetCourseById(int id);
    void DeleteCourse(int id);
    void ModifyCourse(Course courseModified);
    void AddPrerequisite(int courseId, int prerequisiteId);
    void RemovePrerequisite(int courseId, int prerequisiteId);
    IEnumerable<Course> GetPrerequisites(int courseId);
}