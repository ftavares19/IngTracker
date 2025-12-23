using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Carrera> Carreras { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Titulo> Titulos { get; set; }
    public DbSet<UsuarioMateria> UsuariosMaterias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}