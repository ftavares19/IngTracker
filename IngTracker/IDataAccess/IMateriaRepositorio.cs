using Domain;

namespace IDataAccess;

public interface IMateriaRepositorio : IRepositorio<Materia>
{
    Materia? ObtenerConPrevias(int id);
    IEnumerable<Materia> ObtenerPorCarrera(int carreraId);
}
