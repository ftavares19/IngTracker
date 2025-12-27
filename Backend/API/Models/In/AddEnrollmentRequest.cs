using Domain;

namespace API.Models.In;

public class AddEnrollmentRequest
{
    public int CourseId { get; set; }
    public Status Status { get; set; }
    public int? Grade { get; set; }
    public DateTime? ApprovalDate { get; set; }

    public Enrollment ToEntity()
    {
        return new Enrollment
        {
            CourseId = CourseId,
            Status = Status,
            Grade = Grade,
            ApprovalDate = ApprovalDate
        };
    }
}
