using Domain;

namespace API.Models.Out;

public class GetCoursesResponse
{
    public List<CourseDto> Courses { get; set; }

    public GetCoursesResponse(List<Course> courses)
    {
        Courses = courses.Select(c => new CourseDto(c)).ToList();
    }
}
