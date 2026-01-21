using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class EnrollmentRepository(AppDbContext context) : IEnrollmentRepository
{
    public Enrollment Add(Enrollment enrollment)
    {
        context.Enrollments.Add(enrollment);
        context.SaveChanges();
        return enrollment;
    }

    public Enrollment Get(int id)
    {
        return context.Enrollments.Find(id);
    }
    
    public IEnumerable<Enrollment> GetAll()
    {
        return context.Enrollments.ToList();
    }

    public IEnumerable<Enrollment> GetByCourseId(int courseId)
    {
        return context.Enrollments
            .Where(e => e.CourseId == courseId)
            .ToList();
    }

    public void Delete(int id)
    {
        var enrollment = context.Enrollments.Find(id);

        if (enrollment == null)
        {
            throw new ExceptionRepository("Enrollment not found");
        }
        
        context.Enrollments.Remove(enrollment);
        context.SaveChanges();
    }

    public void Modify(Enrollment enrollmentModified)
    {
        var enrollment = context.Enrollments.Find(enrollmentModified.Id);

        if (enrollment == null)
        {
            throw new ExceptionRepository("Enrollment not found");
        }

        context.Entry(enrollment).CurrentValues.SetValues(enrollmentModified);
        context.SaveChanges();
    }
}