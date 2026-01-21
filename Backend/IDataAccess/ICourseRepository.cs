using Domain;

namespace IDataAccess;

public interface ICourseRepository
{
    Course Add(Course course);
    public Course Get(int id);
    public Course GetWithPrerequisites(int id);
    public IEnumerable<Course> GetAll();
    public IEnumerable<Course> GetByDegree(int degreeId);
    public IEnumerable<Course> GetBySemester(int degreeId, Semester semester);
    public void Delete(int id);
    void Modify(Course courseModified);
}