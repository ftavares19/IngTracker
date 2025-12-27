using Domain;

namespace API.Models.Out;

public class GetDegreesResponse
{
    public List<DegreeDto> Degrees { get; set; }

    public GetDegreesResponse(List<Degree> degrees)
    {
        Degrees = degrees.Select(d => new DegreeDto(d)).ToList();
    }
}