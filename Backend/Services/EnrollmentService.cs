using Domain;
using IDataAccess;
using IServices;

namespace Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;
    
    public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
    }
    
    public Enrollment AddEnrollment(Enrollment enrollment)
    {
        return _enrollmentRepository.Add(enrollment);
    }
    
    public IEnumerable<Enrollment> GetAllEnrollments()
    {
        return _enrollmentRepository.GetAll();
    }
    
    public Enrollment GetEnrollmentById(int id)
    {
        return _enrollmentRepository.Get(id);
    }
    
    public void DeleteEnrollment(int id)
    {
        _enrollmentRepository.Delete(id);
    }
    
    public void ModifyEnrollment(Enrollment enrollmentModified)
    {
        _enrollmentRepository.Modify(enrollmentModified);
    }

    public bool CanEnrollCourse(int courseId)
    {
        return ArePrerequisitesMet(courseId);
    }

    public bool ArePrerequisitesMet(int courseId)
    {
        var course = _courseRepository.GetWithPrerequisites(courseId);
        
        if (course?.Prerequisites == null || !course.Prerequisites.Any())
            return true;

        var allEnrollments = _enrollmentRepository.GetAll().ToList();
        
        foreach (var prerequisite in course.Prerequisites)
        {
            var prerequisiteEnrollment = allEnrollments
                .FirstOrDefault(e => e.CourseId == prerequisite.Id);
            
            if (prerequisiteEnrollment == null || prerequisiteEnrollment.Status != Status.Passed)
                return false;
        }
        
        return true;
    }

    public IEnumerable<Course> GetAvailableCourses(int degreeId)
    {
        var allCourses = _courseRepository.GetByDegree(degreeId);
        var availableCourses = new List<Course>();
        
        foreach (var course in allCourses)
        {
            if (ArePrerequisitesMet(course.Id))
            {
                var enrollment = _enrollmentRepository.GetByCourseId(course.Id).FirstOrDefault();
                if (enrollment == null || enrollment.Status == Status.Pending)
                {
                    availableCourses.Add(course);
                }
            }
        }
        
        return availableCourses;
    }

    public IEnumerable<Course> GetAvailableCoursesForSemester(int degreeId, Semester semester)
    {
        var semesterCourses = _courseRepository.GetBySemester(degreeId, semester);
        var availableCourses = new List<Course>();
        
        foreach (var course in semesterCourses)
        {
            if (ArePrerequisitesMet(course.Id))
            {
                var enrollment = _enrollmentRepository.GetByCourseId(course.Id).FirstOrDefault();
                if (enrollment == null || enrollment.Status == Status.Pending)
                {
                    availableCourses.Add(course);
                }
            }
        }
        
        return availableCourses;
    }
}