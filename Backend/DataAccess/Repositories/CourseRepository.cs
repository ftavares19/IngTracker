using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Exceptions;

namespace DataAccess.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;
    
    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Course Add(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
        return course;
    }

    public Course Get(int id)
    {
        return _context.Courses.Find(id);
    }
    
    public IEnumerable<Course> GetAll()
    {
        return _context.Courses.ToList();
    }

    public void Delete(int id)
    {
        var course = _context.Courses.Find(id);

        if (course == null)
        {
            throw new ExceptionRepository("Course not found");
        }
        
        _context.Courses.Remove(course);
        _context.SaveChanges();
    }

    public void Modify(Course courseModified)
    {
        var course = _context.Courses.Find(courseModified.Id);

        if (course == null)
        {
            throw new ExceptionRepository("Course not found");
        }

        _context.Entry(course).CurrentValues.SetValues(courseModified);
        _context.SaveChanges();
    }
}