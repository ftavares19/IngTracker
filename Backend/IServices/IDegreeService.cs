using Domain;

namespace IServices;

public interface IDegreeService
{
    Degree AddDegree(Degree degree);
    IEnumerable<Degree> GetAllDegrees();
    Degree GetDegreeById(int id);
    void DeleteDegree(int id);
    void ModifyDegree(Degree degreeModified);
}