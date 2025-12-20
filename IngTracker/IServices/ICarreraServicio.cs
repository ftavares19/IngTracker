using Domain;

namespace IServices;

public interface ICarreraServicio
{
    Carrera Obtener(int id);
    IEnumerable<Carrera> ObtenerTodas();
    Carrera Crear(string nombre, string descripcion);
    void Modificar(int id, string nombre, string descripcion);
    void Eliminar(int id);
    Carrera ObtenerCompleta(int id);
    IEnumerable<Materia> ObtenerMaterias(int carreraId);
}
