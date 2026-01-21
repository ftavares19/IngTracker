using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

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

    public Course GetWithPrerequisites(int id)
    {
        return _context.Courses
            .Include(c => c.Prerequisites)
            .FirstOrDefault(c => c.Id == id);
    }
    
    public IEnumerable<Course> GetAll()
    {
        return _context.Courses.ToList();
    }

    public IEnumerable<Course> GetByDegree(int degreeId)
    {
        return _context.Courses
            .Where(c => c.DegreeId == degreeId)
            .ToList();
    }

    public IEnumerable<Course> GetBySemester(int degreeId, Semester semester)
    {
        return _context.Courses
            .Where(c => c.DegreeId == degreeId && c.Semester == semester)
            .ToList();
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