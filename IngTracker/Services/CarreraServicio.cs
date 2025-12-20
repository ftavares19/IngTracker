using DataAccess.Context;
using Domain;
using IDataAccess;
using IServices;

namespace Services;

public class CarreraServicio(
    ICarreraRepositorio carreraRepo,
    IMateriaRepositorio materiaRepo,
    AppDbContext context) : ICarreraServicio
{
    private readonly ICarreraRepositorio _carreraRepo = carreraRepo;
    private readonly IMateriaRepositorio _materiaRepo = materiaRepo;
    private readonly AppDbContext _context = context;

    public Carrera Obtener(int id)
    {
        return _carreraRepo.Obtener(id);
    }

    public IEnumerable<Carrera> ObtenerTodas()
    {
        return _carreraRepo.ObtenerTodos();
    }

    public Carrera Crear(string nombre, string descripcion)
    {
        var carrera = new Carrera
        {
            Nombre = nombre,
            Descripcion = descripcion
        };

        _carreraRepo.Agregar(carrera);
        _context.SaveChanges();

        return carrera;
    }

    public void Modificar(int id, string nombre, string descripcion)
    {
        var carrera = _carreraRepo.Obtener(id);

        carrera.Nombre = nombre;
        carrera.Descripcion = descripcion;

        _carreraRepo.Modificar(carrera);
        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        _carreraRepo.Eliminar(id);
        _context.SaveChanges();
    }

    public Carrera ObtenerCompleta(int id)
    {
        var carrera = _carreraRepo.ObtenerCompleta(id);
        if (carrera == null)
        {
            throw new IDataAccess.Excepciones.ExcepcionRepositorio($"No se encontró la carrera con id {id}");
        }
        return carrera;
    }

    public IEnumerable<Materia> ObtenerMaterias(int carreraId)
    {
        return _materiaRepo.ObtenerPorCarrera(carreraId);
    }
}
