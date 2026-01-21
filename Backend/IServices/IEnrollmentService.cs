using Domain;

namespace IServices;

public interface IEnrollmentService
{
    Enrollment AddEnrollment(Enrollment enrollment);
    IEnumerable<Enrollment> GetAllEnrollments();
    Enrollment GetEnrollmentById(int id);
    void DeleteEnrollment(int id);
    void ModifyEnrollment(Enrollment enrollmentModified);
    bool CanEnrollCourse(int courseId);
    IEnumerable<Course> GetAvailableCourses(int degreeId);
    IEnumerable<Course> GetAvailableCoursesForSemester(int degreeId, Semester semester);
    bool ArePrerequisitesMet(int courseId);
}