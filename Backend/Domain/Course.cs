namespace Domain;

public class Course
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public Semester Semester { get; set; }
    public int DegreeId { get; set; }
    public Degree Degree { get; set; }
    public List<Course> Prerequisites { get; set; }
    public List<Enrollment> Enrollments { get; set; }
}