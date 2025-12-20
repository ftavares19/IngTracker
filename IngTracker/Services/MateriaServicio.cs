using DataAccess.Context;
using Domain;
using IDataAccess;
using IDataAccess.Excepciones;
using IServices;

namespace Services;

public class MateriaServicio(
    IMateriaRepositorio materiaRepo,
    ICarreraRepositorio carreraRepo,
    AppDbContext context) : IMateriaServicio
{
    private readonly IMateriaRepositorio _materiaRepo = materiaRepo;
    private readonly ICarreraRepositorio _carreraRepo = carreraRepo;
    private readonly AppDbContext _context = context;

    public Materia Obtener(int id)
    {
        return _materiaRepo.Obtener(id);
    }

    public IEnumerable<Materia> ObtenerTodas()
    {
        return _materiaRepo.ObtenerTodos();
    }

    public IEnumerable<Materia> ObtenerPorCarrera(int carreraId)
    {
        return _materiaRepo.ObtenerPorCarrera(carreraId);
    }

    public Materia Crear(string codigo, string nombre, Semestre semestre, int carreraId)
    {
        if (!_carreraRepo.Existe(carreraId))
        {
            throw new ExcepcionRepositorio("La carrera especificada no existe");
        }

        var materia = new Materia
        {
            Codigo = codigo,
            Nombre = nombre,
            Semestre = semestre,
            CarreraId = carreraId
        };

        _materiaRepo.Agregar(materia);
        _context.SaveChanges();

        return materia;
    }

    public void Modificar(int id, string codigo, string nombre, Semestre semestre, int carreraId)
    {
        var materia = _materiaRepo.Obtener(id);

        if (!_carreraRepo.Existe(carreraId))
        {
            throw new ExcepcionRepositorio("La carrera especificada no existe");
        }

        materia.Codigo = codigo;
        materia.Nombre = nombre;
        materia.Semestre = semestre;
        materia.CarreraId = carreraId;

        _materiaRepo.Modificar(materia);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        _materiaRepo.Eliminar(id);
        _context.SaveChanges();
    }

    public Materia ObtenerConPrevias(int id)
    {
        var materia = _materiaRepo.ObtenerConPrevias(id);
        if (materia == null)
        {
            throw new ExcepcionRepositorio($"No se encontró la materia con id {id}");
        }
        return materia;
    }

    public void AsignarPrevia(int materiaId, int materiaIdPrevia)
    {
        var materia = _materiaRepo.ObtenerConPrevias(materiaId);
        var materiaPrevia = _materiaRepo.Obtener(materiaIdPrevia);

        if (materia == null || materiaPrevia == null)
        {
            throw new ExcepcionRepositorio("Una de las materias no existe");
        }

        if (materia.Previas == null)
        {
            materia.Previas = new List<Materia>();
        }

        if (materia.Previas.Any(p => p.Id == materiaIdPrevia))
        {
            throw new ExcepcionRepositorio("La materia previa ya está asignada");
        }

        materia.Previas.Add(materiaPrevia);
        _materiaRepo.Modificar(materia);
        _context.SaveChanges();
    }

    public void RemoverPrevia(int materiaId, int materiaIdPrevia)
    {
        var materia = _materiaRepo.ObtenerConPrevias(materiaId);

        if (materia == null)
        {
            throw new ExcepcionRepositorio("La materia no existe");
        }

        if (materia.Previas == null)
        {
            return;
        }

        var previa = materia.Previas.FirstOrDefault(p => p.Id == materiaIdPrevia);
        if (previa != null)
        {
            materia.Previas.Remove(previa);
            _materiaRepo.Modificar(materia);
            _context.SaveChanges();
        }
    }
}
