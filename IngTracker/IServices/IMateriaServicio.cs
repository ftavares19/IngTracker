using Domain;

namespace IServices;

public interface IMateriaServicio
{
    Materia Obtener(int id);
    IEnumerable<Materia> ObtenerTodas();
    IEnumerable<Materia> ObtenerPorCarrera(int carreraId);
    Materia Crear(string codigo, string nombre, Semestre semestre, int carreraId);
    void Modificar(int id, string codigo, string nombre, Semestre semestre, int carreraId);
    void Eliminar(int id);
    Materia ObtenerConPrevias(int id);
    void AsignarPrevia(int materiaId, int materiaIdPrevia);
    void RemoverPrevia(int materiaId, int materiaIdPrevia);
}
