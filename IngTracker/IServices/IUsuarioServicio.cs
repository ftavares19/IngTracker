using Domain;

namespace IServices;

public interface IUsuarioServicio
{
    Usuario Obtener(int id);
    IEnumerable<Usuario> ObtenerTodos();
    Usuario ObtenerPorEmail(string email);
    Usuario Crear(string nombre, string email, int carreraId);
    void Modificar(int id, string nombre, string email, int carreraId);
    void Eliminar(int id);
    Usuario ObtenerConCarrera(int id);
    Usuario ObtenerConMaterias(int id);
    IEnumerable<UsuarioMateria> ObtenerMateriasDelUsuario(int usuarioId);
}
