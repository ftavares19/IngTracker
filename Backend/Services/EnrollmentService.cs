using Domain;
using IDataAccess;
using IServices;

namespace Services;

public class EnrollmentService : IEnrollmentService
{
    public readonly IEnrollmentRepository EnrollmentRepository;
    
    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        EnrollmentRepository = enrollmentRepository;
    }
    
    public Enrollment AddEnrollment(Enrollment enrollment)
    {
        return EnrollmentRepository.Add(enrollment);
    }
    
    public IEnumerable<Enrollment> GetAllEnrollments()
    {
        return EnrollmentRepository.GetAll();
    }
    
    public Enrollment GetEnrollmentById(int id)
    {
        return EnrollmentRepository.Get(id);
    }
    
    public void DeleteEnrollment(int id)
    {
        EnrollmentRepository.Delete(id);
    }
    
    public void ModifyEnrollment(Enrollment enrollmentModified)
    {
        EnrollmentRepository.Modify(enrollmentModified);
    }
}