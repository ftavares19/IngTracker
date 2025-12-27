using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Exceptions;

namespace DataAccess.Repositories;

public class DegreeRepository : IDegreeRepository
{
    private readonly AppDbContext _context;
    
    public DegreeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Degree Add(Degree degree)
    {
        _context.Degrees.Add(degree);
        _context.SaveChanges();
        return degree;
    }

    public Degree Get(int id)
    {
        return _context.Degrees.Find(id);
    }
    
    public IEnumerable<Degree> GetAll()
    {
        return _context.Degrees.ToList();
    }

    public void Delete(int id)
    {
        var degree = _context.Degrees.Find(id);

        if (degree == null)
        {
            throw new ExceptionRepository("Degree not found");
        }
        
        _context.Degrees.Remove(degree);
        _context.SaveChanges();
    }

    public void Modify(Degree degreeModified)
    {
        var degree = _context.Degrees.Find(degreeModified.Id);

        if (degree == null)
        {
            throw new ExceptionRepository("Degree not found");
        }

        _context.Entry(degree).CurrentValues.SetValues(degreeModified);
        _context.SaveChanges();
    }
}