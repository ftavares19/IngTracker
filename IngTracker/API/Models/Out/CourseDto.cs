using Domain;

namespace API.Models.Out;

public class CourseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public Semester Semester { get; set; }
    public int DegreeId { get; set; }

    public CourseDto(Course course)
    {
        Id = course.Id;
        Code = course.Code;
        Name = course.Name;
        Semester = course.Semester;
        DegreeId = course.DegreeId;
    }
}
