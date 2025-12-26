namespace Domain;

public class Degree
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Course> Courses { get; set; }
}
