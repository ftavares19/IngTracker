using System.Linq.Expressions;

namespace IDataAccess;

public interface IRepositorio<T> where T : class
{
    T Obtener(int id);
    IEnumerable<T> ObtenerTodos();
    IEnumerable<T> Buscar(Expression<Func<T, bool>> predicado);
    T Agregar(T entidad);
    void Modificar(T entidad);
    void Eliminar(int id);
    bool Existe(int id);
}
