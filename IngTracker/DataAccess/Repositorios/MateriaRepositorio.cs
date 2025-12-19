using DataAccess.Context;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorios;

public class MateriaRepositorio(AppDbContext context) : Repositorio<Materia>(context), IMateriaRepositorio
{
    public Materia? ObtenerConPrevias(int id)
    {
        return _dbSet
            .Include(m => m.Previas)
            .FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Materia> ObtenerPorCarrera(int carreraId)
    {
        return _dbSet
            .Where(m => m.CarreraId == carreraId)
            .ToList();
    }
}
