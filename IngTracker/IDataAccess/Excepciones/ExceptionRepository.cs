namespace IDataAccess.Excepciones;

public class ExceptionRepository : Exception
{
    public ExceptionRepository(string mensaje) : base(mensaje)
    {
    }
}
