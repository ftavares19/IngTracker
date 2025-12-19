using Domain;

namespace IDataAccess;

public interface ICarreraRepositorio : IRepositorio<Carrera>
{
    Carrera? ObtenerConTitulos(int id);
    Carrera? ObtenerConMaterias(int id);
    Carrera? ObtenerCompleta(int id);
}
