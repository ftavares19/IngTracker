using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Usuarios { get; set; }
    public DbSet<Degree> Carreras { get; set; }
    public DbSet<Course> Materias { get; set; }
    public DbSet<Enrollment> UsuariosMaterias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}