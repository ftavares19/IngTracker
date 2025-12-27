using Domain;

namespace API.Models.Out;

public class EnrollmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public Status Status { get; set; }
    public int? Grade { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public EnrollmentDto(Enrollment enrollment)
    {
        Id = enrollment.Id;
        CourseId = enrollment.CourseId;
        Status = enrollment.Status;
        Grade = enrollment.Grade;
        ApprovalDate = enrollment.ApprovalDate;
    }
}
