using Domain;

namespace API.Models.In;

public class ModifyCourseRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public Semester Semester { get; set; }
    public int DegreeId { get; set; }

    public Course ToEntity()
    {
        return new Course
        {
            Code = Code,
            Name = Name,
            Semester = Semester,
            DegreeId = DegreeId,
            Prerequisites = new List<Course>(),
            Enrollments = new List<Enrollment>()
        };
    }
}
