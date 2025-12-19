using System.Linq.Expressions;
using DataAccess.Context;
using IDataAccess;
using IDataAccess.Excepciones;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositorios;

public class Repositorio<T>(AppDbContext context) : IRepositorio<T> where T : class
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public T Obtener(int id)
    {
        T? entidad = _dbSet.Find(id);
        if (entidad == null)
        {
            throw new ExcepcionRepositorio($"No se encontró la entidad con id {id}");
        }
        return entidad;
    }

    public IEnumerable<T> ObtenerTodos()
    {
        return _dbSet.ToList();
    }

    public IEnumerable<T> Buscar(Expression<Func<T, bool>> predicado)
    {
        return _dbSet.Where(predicado).ToList();
    }

    public T Agregar(T entidad)
    {
        _dbSet.Add(entidad);
        return entidad;
    }

    public void Modificar(T entidad)
    {
        _dbSet.Update(entidad);
    }

    public void Eliminar(int id)
    {
        T? entidad = _dbSet.Find(id);
        if (entidad == null)
        {
            throw new ExcepcionRepositorio($"No se encontró la entidad con id {id}");
        }
        _dbSet.Remove(entidad);
    }

    public bool Existe(int id)
    {
        return _dbSet.Find(id) != null;
    }
}
