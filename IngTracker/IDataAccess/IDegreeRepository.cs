using Domain;

namespace IDataAccess;

public interface IDegreeRepository
{
    Degree Add(Degree degree);
    Degree Get(int id);
    IEnumerable<Degree> GetAll();
    void Delete(int id);
    void Modify(Degree degreeModified);
}