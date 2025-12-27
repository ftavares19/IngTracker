using Domain;

namespace IDataAccess;

public interface ICourseRepository
{
    Course Add(Course course);
    public Course Get(int id);
    public IEnumerable<Course> GetAll();
    public void Delete(int id);

    void Modify(Course courseModified);
}