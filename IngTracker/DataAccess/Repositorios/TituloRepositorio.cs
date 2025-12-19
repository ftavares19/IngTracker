using DataAccess.Context;
using Domain;
using IDataAccess;

namespace DataAccess.Repositorios;

public class TituloRepositorio(AppDbContext context) : Repositorio<Titulo>(context), ITituloRepositorio
{
    public IEnumerable<Titulo> ObtenerPorCarrera(int carreraId)
    {
        return _dbSet
            .Where(t => t.CarreraId == carreraId)
            .ToList();
    }
}
