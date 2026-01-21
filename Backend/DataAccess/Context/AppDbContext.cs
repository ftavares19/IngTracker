using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Degree> Degrees { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasMany(c => c.Prerequisites)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CoursePrerequisites",
                j => j.HasOne<Course>().WithMany().HasForeignKey("PrerequisiteId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("CourseId", "PrerequisiteId");
                    j.ToTable("CoursePrerequisites");
                });
    }
}