using Domain;

namespace IDataAccess;

public interface IUsuarioMateriaRepositorio : IRepositorio<UsuarioMateria>
{
    IEnumerable<UsuarioMateria> ObtenerPorUsuario(int usuarioId);
}
