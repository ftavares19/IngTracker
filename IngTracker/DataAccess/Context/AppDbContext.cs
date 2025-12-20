using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Carrera> Carreras { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Titulo> Titulos { get; set; }
    public DbSet<TituloMateria> TitulosMaterias { get; set; }
    public DbSet<UsuarioMateria> UsuariosMaterias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar TituloMateria (relación muchos-a-muchos)
        modelBuilder.Entity<TituloMateria>()
            .HasKey(tm => new { tm.TituloId, tm.MateriaId });

        modelBuilder.Entity<TituloMateria>()
            .HasOne(tm => tm.Titulo)
            .WithMany(t => t.TitulosMaterias)
            .HasForeignKey(tm => tm.TituloId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TituloMateria>()
            .HasOne(tm => tm.Materia)
            .WithMany(m => m.TitulosMaterias)
            .HasForeignKey(tm => tm.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar Materia - Previas (auto-referencia muchos-a-muchos)
        modelBuilder.Entity<Materia>()
            .HasMany(m => m.Previas)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "MateriaPrevias",
                j => j.HasOne<Materia>().WithMany().HasForeignKey("PreviaId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<Materia>().WithMany().HasForeignKey("MateriaId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasKey("MateriaId", "PreviaId")
            );

        // Configurar otras relaciones para evitar ciclos de cascada
        modelBuilder.Entity<Materia>()
            .HasOne(m => m.Carrera)
            .WithMany(c => c.Materias)
            .HasForeignKey(m => m.CarreraId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Titulo>()
            .HasOne(t => t.Carrera)
            .WithMany(c => c.Titulos)
            .HasForeignKey(t => t.CarreraId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Carrera)
            .WithMany()
            .HasForeignKey(u => u.CarreraId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsuarioMateria>()
            .HasOne(um => um.Usuario)
            .WithMany(u => u.UsuariosMaterias)
            .HasForeignKey(um => um.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UsuarioMateria>()
            .HasOne(um => um.Materia)
            .WithMany(m => m.UsuariosMaterias)
            .HasForeignKey(um => um.MateriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}