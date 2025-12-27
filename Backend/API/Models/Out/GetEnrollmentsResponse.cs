using Domain;

namespace API.Models.Out;

public class GetEnrollmentsResponse
{
    public List<EnrollmentDto> Enrollments { get; set; }

    public GetEnrollmentsResponse(List<Enrollment> enrollments)
    {
        Enrollments = enrollments.Select(e => new EnrollmentDto(e)).ToList();
    }
}
