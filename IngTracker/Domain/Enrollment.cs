namespace Domain;

public class Enrollment
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public Status Status { get; set; }
    public int? Grade { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
}
