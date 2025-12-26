namespace Domain;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int DegreeId { get; set; }
    public Degree Degree { get; set; }
    public List<Enrollment> Enrollments { get; set; }
}
