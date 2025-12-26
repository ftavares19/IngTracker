using Domain;
using IDataAccess;
using IServices;

namespace Services;

public class DegreeService : IDegreeService
{
    public readonly IDegreeRepository DegreeRepository;
    
    public DegreeService(IDegreeRepository degreeRepository)
    {
        DegreeRepository = degreeRepository;
    }
    
    public Degree AddDegree(Degree degree)
    {
        return DegreeRepository.Add(degree);
    }
    
    public IEnumerable<Degree> GetAllDegrees()
    {
        return DegreeRepository.GetAll();
    }
    
    public Degree GetDegreeById(int id)
    {
        return DegreeRepository.Get(id);
    }
    
    public void DeleteDegree(int id)
    {
        DegreeRepository.Delete(id);
    }
    
    public void ModifyDegree(Degree degreeModified)
    {
        DegreeRepository.Modify(degreeModified);
    }
}