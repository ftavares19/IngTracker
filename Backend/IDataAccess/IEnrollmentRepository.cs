using Domain;

namespace IDataAccess;

public interface IEnrollmentRepository
{
    Enrollment Add(Enrollment enrollment);
    Enrollment Get(int id);
    IEnumerable<Enrollment> GetAll();
    IEnumerable<Enrollment> GetByCourseId(int courseId);
    void Delete(int id);
    void Modify(Enrollment enrollmentModified);
}