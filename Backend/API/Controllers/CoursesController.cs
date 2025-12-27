using API.Models.In;
using API.Models.Out;
using Domain;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/courses")]
[ApiController]
public class CoursesController : Controller
{
    private readonly ICourseService _courseService;
    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public ActionResult<AddCourseResponse> AddCourse(AddCourseRequest request)
    {
        var entity = request.ToEntity();
        var courseAdded = _courseService.AddCourse(entity);
        return Ok(new AddCourseResponse(courseAdded));
    }

    [HttpGet]
    public ActionResult<GetCoursesResponse> GetCourses()
    {
        var courses = _courseService.GetAllCourses();
        return Ok(new GetCoursesResponse((List<Course>)courses));
    }

    [HttpGet("{id}")]
    public ActionResult<CourseDto> GetCourseById(int id)
    {
        var course = _courseService.GetCourseById(id);
        return Ok(new CourseDto(course));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCourse(int id)
    {
        _courseService.DeleteCourse(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult ModifyCourse(ModifyCourseRequest request, int id)
    {
        var courseModified = request.ToEntity();
        courseModified.Id = id;
        _courseService.ModifyCourse(courseModified);
        return Ok();
    }

    [HttpPost("{id}/prerequisites")]
    public IActionResult AddPrerequisite(int id, AddPrerequisiteRequest request)
    {
        _courseService.AddPrerequisite(id, request.PrerequisiteId);
        return Ok();
    }

    [HttpDelete("{id}/prerequisites/{prerequisiteId}")]
    public IActionResult RemovePrerequisite(int id, int prerequisiteId)
    {
        _courseService.RemovePrerequisite(id, prerequisiteId);
        return Ok();
    }

    [HttpGet("{id}/prerequisites")]
    public ActionResult<GetPrerequisitesResponse> GetPrerequisites(int id)
    {
        var prerequisites = _courseService.GetPrerequisites(id);
        return Ok(new GetPrerequisitesResponse(prerequisites));
    }
}