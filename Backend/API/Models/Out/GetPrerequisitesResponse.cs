using Domain;

namespace API.Models.Out;

public class GetPrerequisitesResponse
{
    public List<CourseDto> Prerequisites { get; set; }

    public GetPrerequisitesResponse(IEnumerable<Course> prerequisites)
    {
        Prerequisites = prerequisites.Select(p => new CourseDto(p)).ToList();
    }
}
