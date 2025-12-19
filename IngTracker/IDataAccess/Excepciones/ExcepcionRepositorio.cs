namespace IDataAccess.Excepciones;

public class ExcepcionRepositorio : Exception
{
    public ExcepcionRepositorio(string mensaje) : base(mensaje)
    {
    }
}
