using DataAccess.Context;
using Domain;
using IDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorios;

public class CarreraRepositorio(AppDbContext context) : Repositorio<Carrera>(context), ICarreraRepositorio
{
    public Carrera? ObtenerConTitulos(int id)
    {
        return _dbSet
            .Include(c => c.Titulos)
            .FirstOrDefault(c => c.Id == id);
    }

    public Carrera? ObtenerConMaterias(int id)
    {
        return _dbSet
            .Include(c => c.Materias)
            .FirstOrDefault(c => c.Id == id);
    }

    public Carrera? ObtenerCompleta(int id)
    {
        return _dbSet
            .Include(c => c.Titulos)
            .Include(c => c.Materias)
            .FirstOrDefault(c => c.Id == id);
    }
}
