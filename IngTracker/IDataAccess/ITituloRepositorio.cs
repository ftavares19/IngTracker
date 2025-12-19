using Domain;

namespace IDataAccess;

public interface ITituloRepositorio : IRepositorio<Titulo>
{
    IEnumerable<Titulo> ObtenerPorCarrera(int carreraId);
}
