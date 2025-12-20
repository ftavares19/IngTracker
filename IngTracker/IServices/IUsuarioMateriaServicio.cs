using Domain;

namespace IServices;

public interface IUsuarioMateriaServicio
{
    UsuarioMateria InscribirMateria(int usuarioId, int materiaId);
    void ActualizarEstado(int usuarioMateriaId, Estado estado, int? nota = null);
    void AprobarMateria(int usuarioMateriaId, int nota);
    void EliminarInscripcion(int usuarioMateriaId);
    IEnumerable<UsuarioMateria> ObtenerPorUsuario(int usuarioId);
    IEnumerable<Materia> ObtenerMateriasDisponibles(int usuarioId);
    bool PuedeInscribirMateria(int usuarioId, int materiaId);
}
