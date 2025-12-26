using Domain;

namespace API.Models.Out;

public class DegreeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public DegreeDto(Degree degree)
    {
        Id = degree.Id;
        Name = degree.Name;
        Description = degree.Description;
    }
}