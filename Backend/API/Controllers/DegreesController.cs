using API.Models.In;
using API.Models.Out;
using Domain;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/degrees")]
[ApiController]
public class DegreesController : Controller
{
    private readonly IDegreeService _degreeService;
    public DegreesController(IDegreeService degreeService)
    {
        _degreeService = degreeService;
    }

    [HttpPost]
    public ActionResult<AddDegreeResponse> AddDegree(AddDegreeRequest request)
    {
        var entity = request.ToEntity();
        var degreeAdded= _degreeService.AddDegree(entity);
        return Ok(new AddDegreeResponse(degreeAdded));
    }

    [HttpGet]
    public ActionResult<GetDegreesResponse> GetDegrees()
    {
        var degrees = _degreeService.GetAllDegrees();
        return Ok(new GetDegreesResponse((List<Degree>)degrees));
    }

    [HttpGet("{id}")]
    public ActionResult<DegreeDto> GetDegreeById(int id)
    {
        var degree = _degreeService.GetDegreeById(id);
        return Ok(new DegreeDto(degree));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDegree(int id)
    {
        _degreeService.DeleteDegree(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult ModifyDegree(ModifyDegreeRequest request, int id)
    {
        var degreeModified = request.ToEntity();
        degreeModified.Id = id;
        _degreeService.ModifyDegree(degreeModified);
        return Ok();
    }
}