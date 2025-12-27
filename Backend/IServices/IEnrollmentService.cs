using Domain;

namespace IServices;

public interface IEnrollmentService
{
    Enrollment AddEnrollment(Enrollment enrollment);
    IEnumerable<Enrollment> GetAllEnrollments();
    Enrollment GetEnrollmentById(int id);
    void DeleteEnrollment(int id);
    void ModifyEnrollment(Enrollment enrollmentModified);
}