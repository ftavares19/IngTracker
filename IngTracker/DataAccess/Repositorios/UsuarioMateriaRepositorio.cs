using DataAccess.Context;
using Domain;
using IDataAccess;

namespace DataAccess.Repositorios;

public class UsuarioMateriaRepositorio(AppDbContext context) : Repositorio<UsuarioMateria>(context), IUsuarioMateriaRepositorio
{
    public IEnumerable<UsuarioMateria> ObtenerPorUsuario(int usuarioId)
    {
        return _dbSet
            .Where(um => um.UsuarioId == usuarioId)
            .ToList();
    }
}
