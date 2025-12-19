using Domain;

namespace IDataAccess;

public interface IUsuarioRepositorio : IRepositorio<Usuario>
{
    Usuario? ObtenerPorEmail(string email);
    Usuario? ObtenerConCarrera(int id);
    Usuario? ObtenerConMaterias(int id);
}
