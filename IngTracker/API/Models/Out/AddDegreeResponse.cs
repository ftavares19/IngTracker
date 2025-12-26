using Domain;

namespace API.Models.Out;

public class AddDegreeResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public AddDegreeResponse(Degree degree)
    {
        Id = degree.Id;
        Name = degree.Name;
        Description = degree.Description;
    }
}