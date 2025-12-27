using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Exceptions;

namespace DataAccess.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;
    
    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Enrollment Add(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();
        return enrollment;
    }

    public Enrollment Get(int id)
    {
        return _context.Enrollments.Find(id);
    }
    
    public IEnumerable<Enrollment> GetAll()
    {
        return _context.Enrollments.ToList();
    }

    public void Delete(int id)
    {
        var enrollment = _context.Enrollments.Find(id);

        if (enrollment == null)
        {
            throw new ExceptionRepository("Enrollment not found");
        }
        
        _context.Enrollments.Remove(enrollment);
        _context.SaveChanges();
    }

    public void Modify(Enrollment enrollmentModified)
    {
        var enrollment = _context.Enrollments.Find(enrollmentModified.Id);

        if (enrollment == null)
        {
            throw new ExceptionRepository("Enrollment not found");
        }

        _context.Entry(enrollment).CurrentValues.SetValues(enrollmentModified);
        _context.SaveChanges();
    }
}