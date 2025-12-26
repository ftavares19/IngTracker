using Domain;

namespace API.Models.In;

public class ModifyDegreeRequest
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Degree ToEntity()
    {
        return new Degree
        {
            Name = Name,
            Description = Description,
            Courses = new List<Course>()
        };
    }
}