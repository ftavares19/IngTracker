using API.Models.In;
using API.Models.Out;
using Domain;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/enrollments")]
[ApiController]
public class EnrollmentsController : Controller
{
    private readonly IEnrollmentService _enrollmentService;
    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public ActionResult<AddEnrollmentResponse> AddEnrollment(AddEnrollmentRequest request)
    {
        var entity = request.ToEntity();
        var enrollmentAdded = _enrollmentService.AddEnrollment(entity);
        return Ok(new AddEnrollmentResponse(enrollmentAdded));
    }

    [HttpGet]
    public ActionResult<GetEnrollmentsResponse> GetEnrollments()
    {
        var enrollments = _enrollmentService.GetAllEnrollments();
        return Ok(new GetEnrollmentsResponse((List<Enrollment>)enrollments));
    }

    [HttpGet("{id}")]
    public ActionResult<EnrollmentDto> GetEnrollmentById(int id)
    {
        var enrollment = _enrollmentService.GetEnrollmentById(id);
        return Ok(new EnrollmentDto(enrollment));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEnrollment(int id)
    {
        _enrollmentService.DeleteEnrollment(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult ModifyEnrollment(ModifyEnrollmentRequest request, int id)
    {
        var enrollmentModified = request.ToEntity();
        enrollmentModified.Id = id;
        _enrollmentService.ModifyEnrollment(enrollmentModified);
        return Ok();
    }
}