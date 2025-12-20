using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Excepciones;
using IServices;

namespace Services;

public class UsuarioMateriaServicio(
    IUsuarioMateriaRepositorio usuarioMateriaRepo,
    IUsuarioRepositorio usuarioRepo,
    IMateriaRepositorio materiaRepo,
    AppDbContext context) : IUsuarioMateriaServicio
{
    private readonly IUsuarioMateriaRepositorio _usuarioMateriaRepo = usuarioMateriaRepo;
    private readonly IUsuarioRepositorio _usuarioRepo = usuarioRepo;
    private readonly IMateriaRepositorio _materiaRepo = materiaRepo;
    private readonly AppDbContext _context = context;

    public UsuarioMateria InscribirMateria(int usuarioId, int materiaId)
    {
        if (!PuedeInscribirMateria(usuarioId, materiaId))
        {
            throw new ExcepcionRepositorio("No se puede inscribir a esta materia. Verifica las previas.");
        }

        var inscripcionExistente = _usuarioMateriaRepo.ObtenerPorUsuario(usuarioId)
            .FirstOrDefault(um => um.MateriaId == materiaId);

        if (inscripcionExistente != null)
        {
            throw new ExcepcionRepositorio("El usuario ya está inscrito en esta materia");
        }

        var usuarioMateria = new UsuarioMateria
        {
            UsuarioId = usuarioId,
            MateriaId = materiaId,
            Estado = Estado.Cursando,
            FechaInicio = DateTime.Now
        };

        _usuarioMateriaRepo.Agregar(usuarioMateria);
        _context.SaveChanges();

        return usuarioMateria;
    }

    public void ActualizarEstado(int usuarioMateriaId, Estado estado, int? nota = null)
    {
        var usuarioMateria = _usuarioMateriaRepo.Obtener(usuarioMateriaId);

        usuarioMateria.Estado = estado;
        usuarioMateria.Nota = nota;

        if (estado == Estado.Aprobada && !usuarioMateria.FechaAprobacion.HasValue)
        {
            usuarioMateria.FechaAprobacion = DateTime.Now;
        }

        _usuarioMateriaRepo.Modificar(usuarioMateria);
        _context.SaveChanges();
    }

    public void AprobarMateria(int usuarioMateriaId, int nota)
    {
        if (nota < 0 || nota > 100)
        {
            throw new ExcepcionRepositorio("La nota debe estar entre 0 y 100");
        }

        var usuarioMateria = _usuarioMateriaRepo.Obtener(usuarioMateriaId);

        usuarioMateria.Estado = Estado.Aprobada;
        usuarioMateria.Nota = nota;
        usuarioMateria.FechaAprobacion = DateTime.Now;

        _usuarioMateriaRepo.Modificar(usuarioMateria);
        _context.SaveChanges();
    }

    public void EliminarInscripcion(int usuarioMateriaId)
    {
        _usuarioMateriaRepo.Eliminar(usuarioMateriaId);
        _context.SaveChanges();
    }

    public IEnumerable<UsuarioMateria> ObtenerPorUsuario(int usuarioId)
    {
        return _usuarioMateriaRepo.ObtenerPorUsuario(usuarioId);
    }

    public IEnumerable<Materia> ObtenerMateriasDisponibles(int usuarioId)
    {
        var usuario = _usuarioRepo.ObtenerConMaterias(usuarioId);
        if (usuario == null)
        {
            throw new ExcepcionRepositorio("Usuario no encontrado");
        }

        var todasLasMaterias = _materiaRepo.ObtenerPorCarrera(usuario.CarreraId).ToList();
        var materiasAprobadas = usuario.UsuariosMaterias
            .Where(um => um.Estado == Estado.Aprobada)
            .Select(um => um.MateriaId)
            .ToList();

        var materiasInscriptas = usuario.UsuariosMaterias
            .Select(um => um.MateriaId)
            .ToList();

        var materiasDisponibles = new List<Materia>();

        foreach (var materia in todasLasMaterias)
        {
            if (materiasInscriptas.Contains(materia.Id))
                continue;

            var materiaConPrevias = _materiaRepo.ObtenerConPrevias(materia.Id);
            if (materiaConPrevias == null)
                continue;

            bool tienePreviasAprobadas = materiaConPrevias.Previas == null || 
                                         materiaConPrevias.Previas.All(p => materiasAprobadas.Contains(p.Id));

            if (tienePreviasAprobadas)
            {
                materiasDisponibles.Add(materia);
            }
        }

        return materiasDisponibles;
    }

    public bool PuedeInscribirMateria(int usuarioId, int materiaId)
    {
        var usuario = _usuarioRepo.ObtenerConMaterias(usuarioId);
        if (usuario == null)
        {
            return false;
        }

        var materia = _materiaRepo.ObtenerConPrevias(materiaId);
        if (materia == null)
        {
            return false;
        }

        if (materia.Previas == null || !materia.Previas.Any())
        {
            return true;
        }

        var materiasAprobadas = usuario.UsuariosMaterias
            .Where(um => um.Estado == Estado.Aprobada)
            .Select(um => um.MateriaId)
            .ToList();

        return materia.Previas.All(p => materiasAprobadas.Contains(p.Id));
    }
}
