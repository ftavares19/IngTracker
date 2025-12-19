using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorios;

public class UsuarioRepositorio(AppDbContext context) : Repositorio<Usuario>(context), IUsuarioRepositorio
{
    public Usuario? ObtenerPorEmail(string email)
    {
        return _dbSet.FirstOrDefault(u => u.Email == email);
    }

    public Usuario? ObtenerConCarrera(int id)
    {
        return _dbSet
            .Include(u => u.Carrera)
            .FirstOrDefault(u => u.Id == id);
    }

    public Usuario? ObtenerConMaterias(int id)
    {
        return _dbSet
            .Include(u => u.UsuariosMaterias)
                .ThenInclude(um => um.Materia)
            .FirstOrDefault(u => u.Id == id);
    }
}
